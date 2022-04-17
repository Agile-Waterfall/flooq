const {VM} = require('vm2');


const vm = new VM({
    timeout: 1000,
    allowAsync: false,
    sandbox: {
        params: ["a", "b", "c"],
        body: "return a + b + c",
        inputs: [1, 4, 7]
    }
});

const a = [1,2,3,4]
a.map((val) => val+1)


console.log(vm.run("new Function(...params, body)(...inputs)"))

vm.sandbox = {
    params: ["a", "b", "c"],
    body: "return a + b + c",
    inputs: [1000,10,10]
}

console.log(vm.run("new Function(...params, body)(...inputs)"))

