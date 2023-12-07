use std::collections::HashMap;
use std::fs;
use std::io;

fn get_card_rank_one(card: char) -> i32 {
    match card {
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'J' => 11,
        'T' => 10,
        '9' => 9,
        '8' => 8,
        '7' => 7,
        '6' => 6,
        '5' => 5,
        '4' => 4,
        '3' => 3,
        '2' => 2,
        _ => 0,
    }
}

fn get_card_rank_two(card: char) -> i32 {
    match card {
        'A' => 14,
        'K' => 13,
        'Q' => 12,
        'J' => 1,
        'T' => 10,
        '9' => 9,
        '8' => 8,
        '7' => 7,
        '6' => 6,
        '5' => 5,
        '4' => 4,
        '3' => 3,
        '2' => 2,
        _ => 0, 
    }
}

fn classify_hand_one(hand: &str) -> Vec<i32> {
    let mut card_counts = HashMap::new();
    let mut score:Vec<i32> = Vec::new();
    for card in hand.chars() {
        let rank = get_card_rank_one(card);
        *card_counts.entry(rank).or_insert(0) += 1;
    }

    let mut frequencies = card_counts.values().cloned().collect::<Vec<_>>();
    frequencies.sort_unstable();

    let rating = match frequencies.as_slice() {
        [1, 1, 1, 1, 1] => 1,
        [1, 1, 1, 2, ..] => 2,
        [1, 2, 2, ..] => 3,
        [1, 1, 3, ..] => 4,
        [2, 3, ..] => 5,
        [1, 4, ..] => 6,
        [5, ..] => 7,
        _ => 0,
    };

    score.push(rating);
    for card in hand.chars() {
        score.push(get_card_rank_one(card))
    }

    return score;
}

fn classify_hand_two(hand: &str) -> Vec<i32> {
    let mut card_counts = HashMap::new();
    let mut score:Vec<i32> = Vec::new();

    for card in hand.chars() {
        *card_counts.entry(card).or_insert(0) += 1;
    }
    
    let mut joker_count = *card_counts.get(&'J').unwrap_or(&0);
    if joker_count < 5 {
        card_counts.remove(&'J');
    } else {
        joker_count = 0;
    }

    let common_card = card_counts.iter().max_by(|a, b| a.1.cmp(b.1)).unwrap();
    let chars = *common_card.0;
    *card_counts.entry(chars).or_insert(0) += joker_count;
    let mut frequencies = card_counts.values().cloned().collect::<Vec<_>>();

    frequencies.sort_unstable();

    let rating = match frequencies.as_slice() {
        [1, 1, 1, 1, 1] => 1,
        [1, 1, 1, 2, ..] => 2,
        [1, 2, 2, ..] => 3,
        [1, 1, 3, ..] => 4,
        [2, 3, ..] => 5,
        [1, 4, ..] => 6,
        [5, ..] => 7,
        _ => 0,
    };
    
    score.push(rating);
    for card in hand.chars() {
        score.push(get_card_rank_two(card))
    }

    return score;
}


fn main() -> io::Result<()>{
    let content = fs::read_to_string("data/real.txt")?;

    let mut hands:Vec<(&str, i32)> = Vec::new();

    for section in content.split("\r\n") {
        let mut lines = section.lines();
        if let Some(line) = lines.next() {
            let x:Vec<&str> = line
                .split_whitespace()
                .collect();

            hands.push((x[0], x[1].parse().ok().unwrap()));
        }
    }

    //part 1
    let mut sorted_card_one:Vec<(Vec<i32>, i32)> = Vec::new();
    for (hand, score) in &hands {
        sorted_card_one.push((classify_hand_one(hand), *score));
    }

    sorted_card_one.sort_by(|a, b| {
        let mut iter_a = a.0.iter();
        let mut iter_b = b.0.iter();

        loop {
            match (iter_a.next(), iter_b.next()) {
                (Some(x), Some(y)) => {
                    if x != y {
                        return y.cmp(x);
                    }
                },
                _ => ()
            }
        }
    });

    let length = sorted_card_one.len();
    let mut total_part_one = 0;
    let mut index = 0;
    for multiplier in (1..=length).rev() {
        total_part_one += sorted_card_one[index].1 * multiplier as i32;
        index += 1;
    }
    println!("Part One: {}", total_part_one);

    // part 2
    let mut sorted_card_two:Vec<(Vec<i32>, i32)> = Vec::new();
    for (hand, score) in &hands {
        sorted_card_two.push((classify_hand_two(hand), *score));
    }

    sorted_card_two.sort_by(|a, b| {
        let mut iter_a = a.0.iter();
        let mut iter_b = b.0.iter();

        loop {
            match (iter_a.next(), iter_b.next()) {
                (Some(x), Some(y)) => {
                    if x != y {
                        return y.cmp(x);
                    }
                },
                _ => ()
            }
        }
    });

    let length = sorted_card_two.len();
    let mut total_part_two = 0;
    let mut index = 0;
    for multiplier in (1..=length).rev() {
        total_part_two += sorted_card_two[index].1 * multiplier as i32;
        index += 1;
    }
    println!("Part Two: {}", total_part_two);

    Ok(())
}
