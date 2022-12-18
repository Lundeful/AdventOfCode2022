import fs from 'fs';
import { ParseInput, Valve } from './functions';

console.clear();
console.log('\n#################### Running program ####################');

const lines = fs.readFileSync('./input.txt').toString().split('\n');
const testLines = fs.readFileSync('./testInput.txt').toString().split('\n');

const valves = ParseInput(lines);
const testValves = ParseInput(testLines);
const minutes = 30;

function findMaxPressure(valves: Valve[]) {
    const startingValve = valves.find(v => v.name === 'AA')!;
    const queue = [{ valve: startingValve, time: 0, pressure: 0, isOpened: false }];
    const processed = new Set();
    let maxPressure = 0;

    while (queue.length > 0) {
        const { valve, time, pressure, isOpened } = queue.shift()!;

        if (processed.has(valve)) {
            continue;
        }

        processed.add(valve);

        if (time >= minutes) {
            return maxPressure;
        }

        const newPressure = pressure + (isOpened ? valve.flowRate * (minutes - time) : 0);
        maxPressure = Math.max(maxPressure, newPressure);

        // Add the connected valves to the queue
        for (const connectedValve of valve.connections) {
            var nextValve = valves.find(v => v.name == connectedValve)!;
            queue.push({ valve: nextValve, time: time + 2, pressure: newPressure, isOpened: nextValve.flowRate > 0 && true });
            queue.push({ valve: nextValve, time: time + 1, pressure: newPressure, isOpened: false });
        }
    }

    return maxPressure;
}

console.log('TestValves: ' + findMaxPressure(testValves)); // Should be 1651
console.log('\n\n');

console.log('Valves: ' + findMaxPressure(valves));
console.log('\n\n');
