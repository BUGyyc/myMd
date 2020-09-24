//正整数

let sumStrings = function (a, b) {
    a = a.trim();
    b = b.trim();
    if (a.length == 0) return b;
    if (b.length == 0) return a;
    let flagA = (a[0] == "-") ? -1 : 1;
    let flagB = (b[0] == "-") ? -1 : 1;
    if (flagA == flagB) {
        //同负同正
        if (flagA == -1) {//把负号截断
            a = a.substring(1);
            b = b.substring(1);
        }
    } else {
        //一负一正
        //转减法
        if (flagA == 1) {
            b = b.substring(1);//去除负号
            return subStrings(a, b);
        } else {
            a = a.substring(1);//去除负号
            return subStrings(b, a);
        }
    }
    var result = [], count = 0;
    if (a.length < b.length) b = [a, a = b][0];
    b = Array(a.length - b.length + 1).join('0') + b;
    var arrA = a.split('');
    var arrB = b.split('');
    for (var j = 0; j < a.length; j++) {
        var temp = (Number(arrA.pop()) + Number(arrB.pop())) + count;
        temp >= 10 ? [temp, count] = [temp - 10, 1] : count = 0;
        result.push(temp);
    }
    result.push(count);
    return result.reverse().join('').replace(/^0+/, '');
}

let subStrings = function (num1, num2) {
    if (num1 === num2) return '0'
    let isMinus = false
    if (lt(num1, num2)) {
        [num1, num2] = [num2, num1]
        isMinus = true
    }
    let len = Math.max(num1.length, num2.length)
    num1 = num1.padStart(len, 0)
    num2 = num2.padStart(len, 0)
    let flag = 0,
        result = '',
        temp
    for (let i = len - 1; i >= 0; i--) {
        temp = parseInt(num1[i]) - flag - parseInt(num2[i])
        if (temp < 0) {
            result = (10 + temp) + result
            flag = 1
        } else {
            result = temp + result
            flag = 0
        }
    }
    result = (isMinus ? '-' : '') + result.replace(/^0+/, '') //去掉前面多余的0，如"1324"-"1315"
    return result
}

let lt = function (num1, num2) {
    if (num1.length < num2.length) {
        return true
    } else if (num1.length === num2.length) {
        return num1 < num2
    } else {
        return false
    }
}

console.log(sumStrings("111111111111111","1"))
console.log(sumStrings("111111111111111","-1"))
console.log(sumStrings("-10000000000","-3"))
console.log(sumStrings("-999999999","9"))
console.log(sumStrings("333333333333333","-289687245"))
console.log(sumStrings("999999999999999999999999999999999999999999999999999991","1"))