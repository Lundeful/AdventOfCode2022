import fs from 'fs';

console.clear();
console.log('\n#################### Running program ####################');
const cubeLines = fs.readFileSync('./input.txt').toString().split('\n');
const testCubeLines = fs.readFileSync('./testInput.txt').toString().split('\n');

interface Cube {
    x: number;
    y: number;
    z: number;
}

const cubes: Cube[] = cubeLines.map((line, index) => {
    const parts = line.split(',');
    const cube: Cube = {
        x: Number.parseInt(parts[0]),
        y: Number.parseInt(parts[1]),
        z: Number.parseInt(parts[2]),
    };
    return cube;
});

const testCubes: Cube[] = testCubeLines.map((line, index) => {
    const parts = line.split(',');
    const cube: Cube = {
        x: Number.parseInt(parts[0]),
        y: Number.parseInt(parts[1]),
        z: Number.parseInt(parts[2]),
    };
    return cube;
});

const cubesInlude = (cubes: Cube[], cube: Cube) => cubes.some(c => c.x == cube.x && c.y == cube.y && c.z == cube.z);

const getAirBubbles = (cubes: Cube[]) => {
    const queue: Cube[] = [];
    const airBubbles: Cube[] = [];
    queue.push({ x: 0, y: 0, z: 0 });

    const maxWith = Math.max.apply(
        Math,
        cubes.map(c => c.x)
    );
    const maxHeight = Math.max.apply(
        Math,
        cubes.map(c => c.y)
    );
    const maxDepth = Math.max.apply(
        Math,
        cubes.map(c => c.z)
    );

    while (queue.length > 0) {
        const pocket: Cube = queue.shift()!;
        const right = { ...pocket, x: pocket.x + 1 };
        const left = { ...pocket, x: pocket.x - 1 };
        const top = { ...pocket, y: pocket.y + 1 };
        const bottom = { ...pocket, y: pocket.y - 1 };
        const forwards = { ...pocket, z: pocket.z + 1 };
        const backwards = { ...pocket, z: pocket.z - 1 };

        if (
            cubesInlude(airBubbles, pocket) ||
            pocket.x < -1 ||
            pocket.y < -1 ||
            pocket.z < -1 ||
            pocket.x > maxWith + 1 ||
            pocket.y > maxHeight + 1 ||
            pocket.z > maxDepth + 1
        ) {
            continue;
        }

        airBubbles.push(pocket);

        if (!cubesInlude(cubes, left)) {
            queue.push(left);
        }

        if (!cubesInlude(cubes, right)) {
            queue.push(right);
        }

        if (!cubesInlude(cubes, top)) {
            queue.push(top);
        }

        if (!cubesInlude(cubes, bottom)) {
            queue.push(bottom);
        }

        if (!cubesInlude(cubes, forwards)) {
            queue.push(forwards);
        }

        if (!cubesInlude(cubes, backwards)) {
            queue.push(backwards);
        }
    }

    return airBubbles;
};

const getExposedSidesPart1 = (cubes: Cube[]) => {
    let exposedSides = 0;
    cubes.forEach(cube => {
        const leftPocket = { x: cube.x - 1, y: cube.y, z: cube.z };
        if (!cubesInlude(cubes, leftPocket)) {
            exposedSides++;
        }

        const rightPocket = { x: cube.x + 1, y: cube.y, z: cube.z };
        if (!cubesInlude(cubes, rightPocket)) {
            exposedSides++;
        }

        const topPocket = { x: cube.x, y: cube.y + 1, z: cube.z };
        if (!cubesInlude(cubes, topPocket)) {
            exposedSides++;
        }

        const bottomPocket = { x: cube.x, y: cube.y - 1, z: cube.z };
        if (!cubesInlude(cubes, bottomPocket)) {
            exposedSides++;
        }

        const forwardsPocket = { x: cube.x, y: cube.y, z: cube.z + 1 };
        if (!cubesInlude(cubes, forwardsPocket)) {
            exposedSides++;
        }

        const backwardsPocket = { x: cube.x, y: cube.y, z: cube.z - 1 };
        if (!cubesInlude(cubes, backwardsPocket)) {
            exposedSides++;
        }
    });

    return exposedSides;
};

const getExposedSidesPart2 = (cubes: Cube[]) => {
    const airBubbles = getAirBubbles(cubes);
    let exposedSides = 0;
    cubes.forEach(cube => {
        const leftPocket = { x: cube.x - 1, y: cube.y, z: cube.z };
        if (cubesInlude([...airBubbles], leftPocket)) {
            exposedSides++;
        }

        const rightPocket = { x: cube.x + 1, y: cube.y, z: cube.z };
        if (cubesInlude([...airBubbles], rightPocket)) {
            exposedSides++;
        }

        const topPocket = { x: cube.x, y: cube.y + 1, z: cube.z };
        if (cubesInlude([...airBubbles], topPocket)) {
            exposedSides++;
        }

        const bottomPocket = { x: cube.x, y: cube.y - 1, z: cube.z };
        if (cubesInlude([...airBubbles], bottomPocket)) {
            exposedSides++;
        }

        const forwardsPocket = { x: cube.x, y: cube.y, z: cube.z + 1 };
        if (cubesInlude([...airBubbles], forwardsPocket)) {
            exposedSides++;
        }

        const backwardsPocket = { x: cube.x, y: cube.y, z: cube.z - 1 };
        if (cubesInlude([...airBubbles], backwardsPocket)) {
            exposedSides++;
        }
    });

    return exposedSides;
};

console.log('Part 1 - Total exposed test sides: ' + getExposedSidesPart1(testCubes)); // 64 for part 1
console.log('Part 1 - Total exposed real sides: ' + getExposedSidesPart1(cubes)); // 4474
console.log();
console.log('Part 2 - Total exposed test sides: ' + getExposedSidesPart2(testCubes)); // 58 for part 2
console.log('Part 2 - Total exposed real sides: ' + getExposedSidesPart2(cubes)); // 2518
