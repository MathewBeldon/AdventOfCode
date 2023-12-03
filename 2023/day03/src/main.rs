use std::fs::File;
use std::io::{self, BufRead, Result};
use std::path::Path;
use std::collections::HashMap;

fn is_symbol(c: char) -> bool {
    !c.is_alphanumeric() && !c.is_whitespace() && c != '.'
}

fn check_adjacent_symbols(arr: &Vec<Vec<char>>, row: i32, col: i32) -> Option<(char, i32, i32)> {
    let row_count = arr.len() as i32;
    let col_count = arr[0].len() as i32;

    let adjacent_positions = [
        (row - 1, col),
        (row + 1, col),
        (row, col - 1),
        (row, col + 1),
        (row - 1, col - 1),
        (row - 1, col + 1),
        (row + 1, col - 1),
        (row + 1, col + 1)
    ];

    for &(r, c) in &adjacent_positions {
        if r >= 0 && r < row_count 
            && c >= 0 && c < col_count{
                if is_symbol(arr[r as usize][c as usize]) { 
                    return Some((arr[r as usize][c as usize], r, c));
                }
        }
    }
    return None;
}

fn get_first_index(arr: &Vec<Vec<char>>, row: i32, col: i32) -> i32 {
    let mut index = col;
    for c in (0..col).rev() {
        if !arr[row as usize][c as usize].is_digit(10) { 
            return index;
        }
        index = c;
    }
    return index;
}

fn get_last_index(arr: &Vec<Vec<char>>, row: i32, col: i32) -> i32 {
    let mut index = col;
    let col_count = arr[0].len() as i32;

    for c in col..col_count {
        if !arr[row as usize][c as usize].is_digit(10) { 
            return index;
        }
        index = c;
    }
    return index;
}

fn read_2d_char_array_from_file(filename: &str) -> Result<Vec<Vec<char>>> {
    let path = Path::new(filename);
    let file = File::open(path)?;
    let reader = io::BufReader::new(file);

    let mut array_2d = Vec::new();

    for line in reader.lines() {
        let line = line?;
        let char_vec: Vec<char> = line.chars().collect();
        array_2d.push(char_vec);
    }

    Ok(array_2d)
}

fn main() -> Result<()> {
    let array_2d = read_2d_char_array_from_file("data/real.txt")?;

    let mut total_part_one = 0;
    let mut total_part_two = 0;
    let mut gears: HashMap<String, Vec<u32>> = HashMap::new();

    for r in 0..array_2d.len()  {
        let mut c = 0;
        while c < array_2d[r].len() {
            if array_2d[r][c].is_digit(10){
                match check_adjacent_symbols(&array_2d, r as i32, c as i32) {
                    Some((symbol, symbol_row, symbol_col)) => {
                        // part 1
                        let first_digit_index = get_first_index(&array_2d, r as i32, c as i32);
                        let last_digit_index = get_last_index(&array_2d, r as i32, c as i32);

                        let mut engine_part = 0;
                        for i in first_digit_index..=last_digit_index {
                            engine_part = (engine_part * 10) + array_2d[r][i as usize].to_digit(10).unwrap_or(0);
                        }
                        total_part_one += engine_part;

                        c = last_digit_index as usize;

                        // part 2
                        if symbol == '*' {
                            gears.entry(format!("{}{}{}", symbol, symbol_row, symbol_col)).or_insert_with(Vec::new).push(engine_part);
                        }
                    },
                    _ => {}
                }
            }
            c += 1;
        }
    }
    println!("Total Part One: {}", total_part_one);
 
    for (_, values) in &gears {
        if values.len() >= 2 {
            total_part_two += values.iter().product::<u32>();
        }
    }
    println!("Total Part Two: {}", total_part_two);
    Ok(())
}