use std::fs::File;
use std::io::{self, BufRead};

fn main() -> io::Result<()> {
    let file = File::open("data/real.txt")?;
    let reader = io::BufReader::new(file);

    let (mut left_numbers, mut right_numbers): (Vec<i32>, Vec<i32>) = reader
        .lines()
        .filter_map(|line| line.ok())
        .filter(|line| !line.trim().is_empty())
        .filter_map(|line| {
            let parts: Vec<&str> = line.trim().split_whitespace().collect();
            match (parts[0].parse::<i32>(), parts[1].parse::<i32>()) {
                (Ok(left), Ok(right)) => Some((left, right)),
                (_, _) => todo!()
            }
        })
        .unzip();

    left_numbers.sort();
    right_numbers.sort();

    let mut total1: i32 = 0;
    for (left_value, right_value) in left_numbers.iter().zip(right_numbers.iter()) {
        total1 += i32::abs(left_value - right_value)
    }
    println!("Part 1: {}", total1);

    let mut total2: i32 = 0;
    for &left_value in &left_numbers {
        let mut occurance:i32 = 0;

        for &right_value in &right_numbers {
            if right_value > left_value {
                break;
            }

            if left_value == right_value {
                occurance += 1;
            }
        }

        total2 += left_value * occurance;
    }
    println!("Part 2: {}", total2);

    Ok(())
}