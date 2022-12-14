import fs from 'fs';
const input = fs.readFileSync('../testInput.txt').toString();
const lines = input.split('\n');
const indices = [];
let pairIndex = 1;
const isAllNumbers = (arr) => {
    console.log(arr);
    return arr.every(el => Number.isInteger(el));
};
const evaluateNumberArrays = (left, right) => {
    for (let index = 0; index < left.length; index++) {
        if (index >= right.length)
            return false;
        const leftEl = left[index];
        const rightEl = right[index];
        if (leftEl > rightEl)
            return false;
    }
    return true;
};
for (let i = 0; i < lines.length; i++) {
    const top = lines[i++];
    const bottom = lines[i++];
    pairIndex++;
    if (Array.isArray(eval(top)) && Array.isArray(eval(bottom))) {
        const left = eval(top);
        const right = eval(bottom);
        if (isAllNumbers(left) && isAllNumbers(right)) {
            console.log("All numbers array");
            if (evaluateNumberArrays(left, right)) {
                indices.push(pairIndex);
            }
        }
    }
}
console.log(indices);
if (indices.length > 0)
    console.log(indices.reduce((a, b) => a + b));
//# sourceMappingURL=index.js.map