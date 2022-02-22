var readline = require('readline');

var rl = readline.createInterface(
    process.stdin, process.stdout);


function sleep(ms) {
    return new Promise((resolve) => {
        setTimeout(resolve, ms);
    });
}

(async () => {
    try {
        while (true) {
            await sleep(1000);

            var answer = await new Promise(resolve => {
                rl.question('What is your name? ', resolve);
            });
            console.log("Welcome: ", answer);
            
        }
    } catch (e) {
        // Deal with the fact the chain failed
    }
})();


