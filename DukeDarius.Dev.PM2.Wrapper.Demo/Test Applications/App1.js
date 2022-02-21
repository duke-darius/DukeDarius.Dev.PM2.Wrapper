

function sleep(ms) {
    return new Promise((resolve) => {
        setTimeout(resolve, ms);
    });
}

(async () => {
    try {
        while (true) {
            await sleep(1000);
            console.log("Hello :)");
        }
    } catch (e) {
        // Deal with the fact the chain failed
    }
})();


