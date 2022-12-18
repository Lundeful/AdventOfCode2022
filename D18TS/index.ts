import fs from 'fs';

console.clear();
console.log('\n#################### Running program ####################');
const cubeLines = fs.readFileSync('./input.txt').toString().split('\n');

interface Cube {
    x: number;
    y: number;
    z: number;
    exposedSides: number;
}

const cubes: Cube[] = [];

cubeLines.forEach((line, index) => {
    const parts = line.split(',');
    const cube: Cube = {
        x: Number.parseInt(parts[0]),
        y: Number.parseInt(parts[1]),
        z: Number.parseInt(parts[2]),
        exposedSides: 0,
    };
    cubes.push(cube);
});

cubes.forEach((cube, i) => {
    let exposedSides = 6;
    // Check left
    const left = cubes.some(compareCube => compareCube.x == cube.x - 1 && compareCube.y == cube.y && compareCube.z == cube.z);

    if (left) {
        console.log('Cube ' + i + ' touching left ');
        exposedSides--;
    }

    // Check right
    const right = cubes.some(compareCube => compareCube.x == cube.x + 1 && compareCube.y == cube.y && compareCube.z == cube.z);

    if (right) {
        console.log('Cube ' + i + ' touching right ');
        exposedSides--;
    }

    // Check top
    const top = cubes.some(compareCube => compareCube.x == cube.x && compareCube.y == cube.y + 1 && compareCube.z == cube.z);

    if (top) {
        console.log('Cube ' + i + ' touching top ');
        exposedSides--;
    }

    // Check bottom
    const bottom = cubes.some(compareCube => compareCube.x == cube.x && compareCube.y == cube.y - 1 && compareCube.z == cube.z);

    if (bottom) {
        console.log('Cube ' + i + ' touching bottom ');
        exposedSides--;
    }

    // Check forwards
    const forwards = cubes.some(compareCube => compareCube.x == cube.x && compareCube.y == cube.y && compareCube.z == cube.z + 1);

    if (forwards) {
        console.log('Cube ' + i + ' touching forwards ');
        exposedSides--;
    }

    // Check backwards
    const backwards = cubes.some(compareCube => compareCube.x == cube.x && compareCube.y == cube.y && compareCube.z == cube.z - 1);

    if (backwards) {
        console.log('Cube ' + i + ' touching backwards ');
        exposedSides--;
    }

    cube.exposedSides = exposedSides;
});

const totalExposedSides = cubes.reduce((acc, cube) => acc + cube.exposedSides, 0);
console.log('Total exposed sides: ' + totalExposedSides);
