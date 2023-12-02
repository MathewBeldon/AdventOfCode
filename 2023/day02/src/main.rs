use std::fs::File;
use std::io::{self, BufRead};

fn main() -> io::Result<()> {
    let file = File::open("data/real.txt")?;
    let reader = io::BufReader::new(file);
    
    let mut part_one_total: u32 = 0;
    let mut part_two_total: i32 = 0;
    let mut game_id = 1;

    for line in reader.lines() {
        let line = line?;
        let part_one = valid_game(&line);
        let part_two: i32 = power_cubes(&line);

        if part_one {
            part_one_total += game_id;
        }

        part_two_total += part_two;
        game_id += 1;
    }

    println!("Part One: {}", part_one_total);
    println!("Part Two: {}", part_two_total);

    Ok(())
}

fn valid_game(input: &str) -> bool {
    let parts: Vec<&str> = input.split(": ").collect();
    let colour_counts = parts[1].split("; ");

    for part in colour_counts {
        for colour_pulls in part.split(", ") {
            let colour_pulls_split: Vec<&str> = colour_pulls.split_whitespace().collect();

            match colour_pulls_split[1] {
                "blue" if colour_pulls_split[0].parse::<i32>().unwrap_or(15) > 14 => return false,
                "green" if colour_pulls_split[0].parse::<i32>().unwrap_or(14) > 13 => return false,
                "red" if colour_pulls_split[0].parse::<i32>().unwrap_or(13) > 12 => return false,
                _ => (),
            }
        }
    }

    return true;
}

fn power_cubes(input: &str) -> i32 {
    let parts: Vec<&str> = input.split(": ").collect();
    let colour_counts = parts[1].split("; ");
    let mut max_blue = 0;
    let mut max_green = 0; 
    let mut max_red = 0;

    for part in colour_counts {
        for colour_pulls in part.split(", ") {
            let colour_pull_split: Vec<&str> = colour_pulls.split_whitespace().collect();

            let count = colour_pull_split[0].parse::<i32>().unwrap_or(-1);

            match colour_pull_split[1] {
                "blue" if count > max_blue => max_blue = count,
                "green" if count > max_green => max_green = count,
                "red" if count > max_red => max_red = count,
                _ => (),
            }
        }
    }

    return max_blue * max_red * max_green;
}