use std::io;
use std::fs;

fn main() -> io::Result<()> {
    let content = fs::read_to_string("data/real.txt")?;

    let mut seeds:Vec<i64> = Vec::new();
    let mut maps:Vec<Vec<(i64, i64, i64)>> = Vec::new();

    for section in content.split("\r\n\r\n") {
        let mut lines = section.lines();
        if let Some(line) = lines.next() {
            if line.starts_with("seeds:") {
                seeds = line
                    .split_whitespace()
                    .filter_map(|s| s.parse::<i64>().ok())
                    .collect();

            } else if line.ends_with("map:") {
                maps.push(lines
                    .flat_map(|line| {
                        let parts: Vec<i64> = line
                            .split_whitespace()
                            .filter_map(|s| s.parse::<i64>().ok())
                            .collect();

                        if parts.len() == 3 { 
                            return Some((parts[0], parts[1], parts[2]));
                        } else { 
                            return None;
                        }
                    }).collect()
                );
            }
        }
    }

    // part 1
    let mut current_mapping = seeds.clone();
    for map in &maps {
        let mut next_mapping = Vec::new();
        for seed in &current_mapping {
            let mut handled = false;
            for (destination, source, range) in map {
                if source <= seed && source + range >= *seed {
                    next_mapping.push(seed - source + destination);
                    handled = true;
                    break;
                }
            }
            if !handled {
                next_mapping.push(*seed);
            }
        }
        current_mapping = next_mapping;

    }

    let part_one = current_mapping
        .iter()
        .min()
        .unwrap();

    println!("part one: {}", part_one);


    // part 2
    let mut current_mapping_range:Vec<(i64, i64)> = Vec::new();
    for seed in (0..seeds.len()).step_by(2) {
        current_mapping_range.push((seeds[seed], seeds[seed] + seeds[seed + 1]))
    }
    
    for map in &maps {
        let mut new_seeds: Vec<(i64, i64)> = Vec::new();
        while current_mapping_range.len() > 0 {
            let (range_start, range_end) = current_mapping_range.pop().unwrap();
            let mut handled = false;

            for (destination, source, range) in map {
                let overlap_start = i64::max(range_start, *source);
                let overlap_end = i64::min(range_end, source + range - 1);

                if overlap_start < overlap_end {
                    new_seeds.push((overlap_start - source + destination, overlap_end - source + destination));
                    if overlap_start > range_start {
                        current_mapping_range.push((range_start, overlap_start - 1));
                    }
                    if overlap_end < range_end {
                        current_mapping_range.push((overlap_end + 1, range_end));
                    }
                    handled = true;
                    break;
                }
            }
            if !handled {
                new_seeds.push((range_start, range_end));

            }
        }
        current_mapping_range = new_seeds;
    }

    let part_two = current_mapping_range
        .iter()
        .map(|(left,_)| left)
        .min()
        .unwrap();

    println!("part two: {}", part_two);
    Ok(())
}