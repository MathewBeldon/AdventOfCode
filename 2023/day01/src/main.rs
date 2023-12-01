
use std::fs::File;
use std::io::{self, BufRead};

fn main() -> io::Result<()> {
    let file = File::open("data/real.txt")?;
    let reader = io::BufReader::new(file);
    let replacements = [
        ("one", "o1e"), 
        ("two", "t2o"),
        ("three", "t3e"), 
        ("four", "f4r"),
        ("five", "f5e"), 
        ("six", "s6x"),
        ("seven", "s7n"), 
        ("eight", "e8n"),
        ("nine", "n9e")];

    let mut total: u32 = 0;

    for line in reader.lines() {
        let mut first_char: Option<char> = None;
        let mut second_char: Option<char> = None;

        let line = line?;

        let mut parsed_line = String::from(line);
        for &(old, new) in replacements.iter() {
            parsed_line = parsed_line.replace(old, new);
        }

        for c in parsed_line.chars() {
            if c.is_digit(10) {
                first_char = Some(c);
                break;
            }
        }
        for c in parsed_line.chars().rev() {
            if c.is_digit(10) {
                second_char = Some(c);
                break;
            }
        }
        
        match (first_char, second_char) {
            (Some(first), Some(second)) => {
                if let (Some(first_digit), Some(second_digit)) = (first.to_digit(10), second.to_digit(10)) {
                    let combined_number = first_digit * 10 + second_digit;
                    total += combined_number;
                }
            },
            _ => println!("One or both characters are missing")
        }
    }

    println!("Total: {}", total);

    Ok(())
}