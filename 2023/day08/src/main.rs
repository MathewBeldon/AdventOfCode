use std::collections::HashMap;
use std::fs::File;
use std::io::{self, BufRead};

struct RingBuffer {
    buffer: Vec<char>,
    current: usize,
}

impl RingBuffer {
    fn new(buffer: Vec<char>) -> RingBuffer {
        RingBuffer {
            buffer,
            current: 0,
        }
    }

    fn next(&mut self) -> &char {
        let item = &self.buffer[self.current];
        self.current = (self.current + 1) % self.buffer.len();
        item
    }

    fn reset(&mut self) {
        self.current = 0;
    }
}

fn main() -> io::Result<()> {
    let path = "data/real.txt";
    let file = File::open(path)?;
    let reader = io::BufReader::new(file);

    let mut map: HashMap<String, (String, String)> = HashMap::new();
    let mut prefix_vec = Vec::new();

    for line in reader.lines() {
        let line = line?;
        if !line.contains('=') {
            prefix_vec.extend(line.chars());
        } else {
            let parts: Vec<&str> = line.split('=').map(|s| s.trim()).collect();
            if parts.len() == 2 {
                let key = parts[0].to_string();
                let value = parts[1]
                    .trim_start_matches('(')
                    .trim_end_matches(')')
                    .split(',')
                    .map(|s| s.trim().to_string())
                    .collect::<Vec<String>>();

                if value.len() == 2 {
                    map.insert(key, (value[0].clone(), value[1].clone()));
                }
            }
        }
    }

    let mut ring_buffer = RingBuffer::new(prefix_vec);

    // part 1
    let mut current_pos = "AAA";
    let mut counter = 0;
    while current_pos != "ZZZ" {
        let direction = ring_buffer.next();
        if direction == &'R' {
            current_pos = &map.get(current_pos).unwrap().1;
        } else {
            current_pos = &map.get(current_pos).unwrap().0;
        }
        counter += 1;
    }
    println!("Part One: {}", counter);


    // part 2
    fn gcd(a: i64, b: i64) -> i64 {
        if b == 0 {
            a
        } else {
            gcd(b, a % b)
        }
    }
    
    fn lcm(a: i64, b: i64) -> i64 {
        (a * b) / gcd(a, b) as i64
    }
    
    fn lcm_of_array(arr: &[i32]) -> i64 {
        arr.iter().fold(1, |acc, &num| lcm(acc as i64, num as i64))
    }

    let mut current_pos_list:Vec<&str> = Vec::new();

    for map_key in map.keys() {
        if map_key.ends_with("A") {
            current_pos_list.push(&map_key);
        }
    }

    let mut listed:Vec<i32> = Vec::new();
    for current in current_pos_list {
        let mut selected = current;
        let mut counter = 0;
        ring_buffer.reset();
        while !selected.ends_with("Z") {
            let direction = ring_buffer.next();
            if direction == &'R' {
                selected = &map.get(selected).unwrap().1;
            } else {
                selected = &map.get(selected).unwrap().0;
            }
            counter += 1;
        }
        listed.push(counter);
    }
    
    println!("Part Two: {:?}", lcm_of_array(&listed));

    Ok(())
}