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

const mixNodes = (nodes: Node[]) => {
    const newNodes = [...nodes];
    for (const node of nodes) {
        const oldNodeIndex = newNodes.indexOf(node);
        newNodes.splice(oldNodeIndex, 1); // Remove node at old index
        newNodes.splice((oldNodeIndex + node.value) % newNodes.length, 0, node); // Insert at new Index
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

console.log('Test total is', getSum(mixNodes(testNodes)));
console.log();
console.log('Real total is', getSum(mixNodes(realNodes)));
