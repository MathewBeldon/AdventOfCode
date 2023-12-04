use std::fs::File;
use std::io::{self, BufRead, BufReader};

fn main() -> io::Result<()> {
    let file = File::open("data/real.txt")?;
    let reader = BufReader::new(file);

    let mut total_part_one = 0;

    let lines: Vec<String> = reader.lines().collect::<Result<_, _>>()?;
    let line_count = lines.len();

    let mut cards = vec![1; line_count];

    let mut card: i32 = 0;

    for line in &lines {

        let mut subtotal = 0;
        let line_str = line;
        let parts: Vec<&str> = line_str.split('|').collect();

        let winning_numbers: Vec<i32> = parts[0]
            .split_whitespace()
            .filter_map(|x| x.parse().ok())
            .collect();

        let scratch_numbers: Vec<i32> = parts[1]
            .split_whitespace()
            .flat_map(|x| x.parse().ok())
            .collect();


        let mut card_count = 0;
        for &num in &winning_numbers {
            if scratch_numbers.contains(&num) {
                // part 1 
                if subtotal == 0 {
                    subtotal = 1;
                } else {
                    subtotal *= 2;
                }

                // part 2
                card_count += 1;
            }
        }
        // part 1
        total_part_one += subtotal;

        // part 2 
        let copies = cards[card as usize];
        for i in card..(card + card_count).min(cards.len() as i32 - 1) {
            cards[i as usize + 1] += copies;
        }

        card += 1;
    }

    println!("total for part one: {}", total_part_one);
    println!("total for part two: {}", cards.iter().sum::<i32>());

    Ok(())
}