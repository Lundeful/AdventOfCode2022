import fs from 'fs';

console.clear();
console.log('\n#################### Running program ####################');
const realLines = fs.readFileSync('./input.txt').toString().split('\n');
const testLines = fs.readFileSync('./testInput.txt').toString().split('\n');
const rounds = 10;
const directions = ['N', 'S', 'W', 'E'];

interface Coordinate {
    x: number;
    y: number;
}

interface Elf {
    id: number;
    currentPos: Coordinate;
    nextPos: Coordinate;
}

const getElves = (lines: string[]): Elf[] => {
    const elves: Elf[] = [];
    let id = 1;
    for (let row = 0; row < lines.length; row++) {
        for (let col = 0; col < lines[row].length; col++) {
            const el = lines[row][col];
            if (el === '#') {
                const pos: Coordinate = { x: col, y: row };
                const elf: Elf = {
                    id: id++,
                    currentPos: pos,
                    nextPos: pos,
                };
                elves.push(elf);
            }
        }
    }
    return elves;
};

const pointsOverlap = (p1: Coordinate, p2: Coordinate) => p1.x === p2.x && p1.y === p2.y;

const isOverlapping = (positions: Coordinate[], pos: Coordinate): boolean => {
    return positions.some(p => pointsOverlap(p, pos));
};

const getNextPosition = (elves: Elf[], elf: Elf): Coordinate => {
    const north: Coordinate = { x: elf.currentPos.x, y: elf.currentPos.y - 1 };
    const south: Coordinate = { x: elf.currentPos.x, y: elf.currentPos.y + 1 };

    const west: Coordinate = { x: elf.currentPos.x - 1, y: elf.currentPos.y };
    const east: Coordinate = { x: elf.currentPos.x + 1, y: elf.currentPos.y };

    const northEast: Coordinate = { x: elf.currentPos.x + 1, y: elf.currentPos.y - 1 };
    const northWest: Coordinate = { x: elf.currentPos.x - 1, y: elf.currentPos.y - 1 };

    const southEast: Coordinate = { x: elf.currentPos.x + 1, y: elf.currentPos.y + 1 };
    const southWest: Coordinate = { x: elf.currentPos.x - 1, y: elf.currentPos.y + 1 };

    const currentElfPositions = elves.map(e => e.currentPos);

    const northIsEmpty = !isOverlapping(currentElfPositions, north);
    const southIsEmpty = !isOverlapping(currentElfPositions, south);

    const eastIsEmpty = !isOverlapping(currentElfPositions, east);
    const westIsEmpty = !isOverlapping(currentElfPositions, west);

    const northEastIsEmpty = !isOverlapping(currentElfPositions, northEast);
    const northWestIsEmpty = !isOverlapping(currentElfPositions, northWest);

    const southEastIsEmpty = !isOverlapping(currentElfPositions, southEast);
    const southWestIsEmpty = !isOverlapping(currentElfPositions, southWest);

    if (northIsEmpty && northEastIsEmpty && eastIsEmpty && southEastIsEmpty && southIsEmpty && southWestIsEmpty && westIsEmpty && northWestIsEmpty) {
        return elf.currentPos;
    }

    let nextPosition: Coordinate | null = null;

    for (let i = 0; i < directions.length && nextPosition === null; i++) {
        const dir = directions[i];

        if (dir === 'N' && northIsEmpty && northEastIsEmpty && northWestIsEmpty) {
            nextPosition = north;
        } else if (dir === 'S' && southIsEmpty && southEastIsEmpty && southWestIsEmpty) {
            nextPosition = south;
        } else if (dir === 'W' && westIsEmpty && northWestIsEmpty && southWestIsEmpty) {
            nextPosition = west;
        } else if (dir === 'E' && eastIsEmpty && northEastIsEmpty && southEastIsEmpty) {
            nextPosition = east;
        }
    }

    return nextPosition ?? elf.currentPos;
};

const printGrid = (elves: Elf[]) => {
    console.log('### GRID ###\n');

    const elfPositions = elves.map(e => e.currentPos);

    const minY = Math.min.apply(
        Math,
        elfPositions.map(p => p.y)
    );

    const maxY = Math.max.apply(
        Math,
        elfPositions.map(p => p.y)
    );

    const minX = Math.min.apply(
        Math,
        elfPositions.map(p => p.x)
    );

    const maxX = Math.max.apply(
        Math,
        elfPositions.map(p => p.x)
    );

    let content = '';

    const padding = 0;
    for (let row = minY - padding; row <= maxY + padding; row++) {
        for (let col = minX - padding; col <= maxX + padding; col++) {
            const elfIsPresent = elves.some(e => pointsOverlap(e.currentPos, { x: col, y: row }));
            content += elfIsPresent ? '#' : '.';
        }

        content += '\n';
    }

    console.log(content);
};

const getEmptyGroundTiles = (elves: Elf[]) => {
    for (let round = 0; round < rounds; round++) {
        // Calculate next move
        elves.forEach(elf => {
            elf.nextPos = getNextPosition([...elves], { ...elf });
        });

        // Execute next move
        elves.forEach(elf => {
            const movesAreOverlapping = elves.some(e => e.id !== elf.id && pointsOverlap(e.nextPos, elf.nextPos));
            if (!movesAreOverlapping) {
                elf.currentPos = { ...elf.nextPos };
            }
        });

        // Change directions for next round
        directions.push(directions.shift()!);
    }

    const minY = Math.min.apply(
        Math,
        elves.map(e => e.currentPos.y)
    );

    const maxY = Math.max.apply(
        Math,
        elves.map(e => e.currentPos.y)
    );

    const minX = Math.min.apply(
        Math,
        elves.map(e => e.currentPos.x)
    );

    const maxX = Math.max.apply(
        Math,
        elves.map(e => e.currentPos.x)
    );

    const width = maxX - minX + 1;
    const height = maxY - minY + 1;
    const numberOfTiles = width * height;
    const numberOfElves = elves.length;
    const emptyTiles = numberOfTiles - numberOfElves;

    console.log('Min X', minX, 'Max X', maxX);
    console.log('Min Y', minY, 'Max Y', maxY);
    console.log('Width', width, 'Height', height);
    console.log('Tiles', numberOfTiles);
    console.log('Elves', numberOfElves);
    return emptyTiles;
};

console.log('Test');
const testElves = getElves(testLines);
const emptyTestTiles = getEmptyGroundTiles(testElves);
const expectedResult = 110;
const success = emptyTestTiles === expectedResult;
console.log('Empty test tiles:', emptyTestTiles, 'Should be:', expectedResult, 'Success:', success);

console.log();
console.log('Real');
const realElves = getElves(realLines);
const emptyRealTiles = getEmptyGroundTiles(realElves);
console.log('Empty real tiles', emptyRealTiles);
// 3709 - Wrong, too low
// 3867 - Wrong
