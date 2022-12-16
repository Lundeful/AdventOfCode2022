import fs from 'fs';

const compareArrays = (leftArr: any[], rightArr: any[]): boolean => {
    if (leftArr === undefined || leftArr.length === 0) console.log('LEFT EMPTY OR UNDEFINED');

    var isCorrect = true;
    for (let i = 0; i < leftArr.length; i++) {
        const left = leftArr[i];
        const right = rightArr[i];

        if (left === undefined) return true;
        if (right === undefined && left !== undefined) return false;

        // if ((left === undefined || left.length === 0) && right !== undefined) return true;
        // if (right === undefined) return false;

        if (Number.isInteger(left) && Number.isInteger(right)) {
            // TWO INTEGERS
            if (left > right) {
                // console.log('Incorrect value at both integers', JSON.stringify(left), JSON.stringify(right));
                return false;
            }
        } else if (Array.isArray(left) && Array.isArray(right)) {
            // TWO ARRAYS
            const validResult = compareArrays(left, right);
            if (!validResult) {
                // console.log('Incorrect value at both arrays', JSON.stringify(left), JSON.stringify(right));
                return false;
            }
        } else if (Array.isArray(left) && Number.isInteger(right)) {
            // ONE ARRAY, LEFT
            const validResult = compareArrays(left, [right]);
            if (!validResult) {
                // console.log('Incorrect value at left array', JSON.stringify(left), JSON.stringify(right));
                return false;
            }
        } else if (Number.isInteger(left) && Array.isArray(right)) {
            // ONE ARRAY, RIGHT
            const validResult = compareArrays([left], right);
            if (!validResult) {
                // console.log('Incorrect value at right array', JSON.stringify(left), JSON.stringify(right));
                return false;
            }
        } else {
            console.log('!! UNHANDLED !!', left, right);
        }
    }

    return isCorrect;
};

function parseInput(lines: string[]): { left: number[]; right: number[] }[] {
    const pairs = [];
    for (let i = 0; i < lines.length; i++) {
        const left: number[] = eval(lines[i++]);
        const right: number[] = eval(lines[i++]);
        const pair = { left, right };
        pairs.push(pair);
    }

    return pairs;
}

function runProgram(pairs: { left: number[]; right: number[] }[]) {
    const indices: number[] = [];
    let pairIndex = 1;

    pairs.forEach((pair, index) => {
        console.log(`\n## Pair ${pairIndex} ##`);
        // console.log(JSON.stringify(pair));

        // if (pair.left.length > pair.right.length) {
        //     return;
        // }

        const { left, right } = pair;
        if (compareArrays(left, right)) {
            indices.push(pairIndex);
            console.log(`## Correct ##`);
        } else {
            console.log(`## Incorrect ##`);
        }

        pairIndex++;
    });

    return indices;
}

console.clear();
console.log('\n#################### Running program ####################');
const lines = fs.readFileSync('../input.txt').toString().split('\n');
const pairs = parseInput(lines);
const indices = runProgram(pairs);

console.log('\n##### RESULTS #####');
console.log('\nIndices', indices);
if (indices.length > 0)
    console.log(
        'Sum:',
        indices.reduce((a, b) => a + b)
    );

// 1163
