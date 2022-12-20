import fs from 'fs';

console.clear();
console.log('\n#################### Running program ####################');

interface Node {
    startingIndex: number;
    value: number;
}

const testNodes: Node[] = fs
    .readFileSync('./testInput.txt')
    .toString()
    .split('\n')
    .map((line, index) => {
        const value = Number.parseInt(line);
        return {
            startingIndex: index,
            value,
        };
    });

const realNodes: Node[] = fs
    .readFileSync('./input.txt')
    .toString()
    .split('\n')
    .map((line, index) => {
        const value = Number.parseInt(line);
        return {
            startingIndex: index,
            value,
        };
    });

const mixNodes = (nodes: Node[], iterations: number = 1) => {
    const newNodes = [...nodes];
    for (let i = 0; i < iterations; i++) {
        for (const node of nodes) {
            const oldNodeIndex = newNodes.indexOf(node);
            newNodes.splice(oldNodeIndex, 1); // Remove node at old index
            newNodes.splice((oldNodeIndex + node.value) % newNodes.length, 0, node); // Insert at new Index
        }
    }
    return newNodes;
};

const getSum = (nodes: Node[]) => {
    const newNodes = [...nodes];
    let index = newNodes.findIndex(n => n.value == 0);
    const first = newNodes[(index + 1000) % newNodes.length].value;
    const second = newNodes[(index + 2000) % newNodes.length].value;
    const third = newNodes[(index + 3000) % newNodes.length].value;
    const sum = first + second + third;
    console.log('Numbers are:', first, second, third);
    return sum;
};

console.log('Part 1');
console.log('Test total is', getSum(mixNodes(testNodes))); // 3
console.log();
console.log('Real total is', getSum(mixNodes(realNodes))); // 3473
console.log();

console.log('Part 2');
const decryptionKey = 811589153;
const decryptedTestNodes = testNodes.map(n => ({ ...n, value: n.value * decryptionKey }));
const decryptedRealNodes = realNodes.map(n => ({ ...n, value: n.value * decryptionKey }));
console.log('Test total is', getSum(mixNodes(decryptedTestNodes, 10))); // 1623178306
console.log();
console.log('Real total is', getSum(mixNodes(decryptedRealNodes, 10))); // 7496649006261
