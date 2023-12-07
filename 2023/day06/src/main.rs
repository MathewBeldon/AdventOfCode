use std::io;
use std::fs;

fn main() -> io::Result<()> {
    let content = fs::read_to_string("data/real.txt")?;

    let mut times:Vec<i64> = Vec::new();
    let mut single_time:i64 = 0;
    let mut distances:Vec<i64> = Vec::new();
    let mut single_distance: i64 = 0;

    for section in content.split("\r\n") {
        let mut lines = section.lines();
        if let Some(line) = lines.next() {
            if line.starts_with("Time:") {
                times = line
                    .split_whitespace()
                    .filter_map(|t| t.parse().ok())
                    .collect();

                single_time = times
                    .iter()
                    .map(|t| t.to_string())
                    .collect::<String>()
                    .parse::<i64>()
                    .unwrap();
            } 
            if line.starts_with("Distance:") {
                distances = line
                    .split_whitespace()
                    .filter_map(|d| d.parse().ok())
                    .collect();

                single_distance = distances
                    .iter()
                    .map(|t| t.to_string())
                    .collect::<String>()
                    .parse::<i64>()
                    .unwrap();
            }
        }
    }

    // part 1
    let mut win_counts:Vec<i64> = Vec::new();
    let mut index = 0;
    for time in &times {
        let mut win_count = 0;
        for held_time in 0..*time {
            if held_time * (time - held_time) > distances[index]{
                win_count += 1;
            }
        }
        win_counts.push(win_count);
        index += 1;
    }
    println!("Part One: {}", win_counts.iter().product::<i64>());


    // part 2
    let mut win_count = 0;
    for held_time in 0..single_time {
        if held_time * (single_time - held_time) > single_distance{
            win_count += 1;
        }
    }
    println!("Part Two: {}", win_count);

    Ok(())
}