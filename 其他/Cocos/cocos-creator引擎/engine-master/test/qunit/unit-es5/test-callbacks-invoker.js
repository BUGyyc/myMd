largeModule('CallbacksInvoker');

test('test', function () {
    var ci = new cc._Test.CallbacksInvoker();

    var cb1 = Callback();
    var cb2 = Callback();
    var cb3 = Callback();
    ci.add('a', cb1);
    strictEqual(ci.hasEventListener('a', function () {}), false, '`has` should return false if the callback not exists');
    strictEqual(ci.hasEventListener('a', cb1), true, '`has` should return true if the callback exists');
    ci.add('a', cb2);
    ci.add('b', cb3);
    ci.add('nil', undefined);

    cb1.enable();
    cb2.enable();
    ci.invoke('a');
    cb1.once('1 should be called');
    cb2.once('2 should be called');

    cb3.enable();
    ci.invoke('b');
    cb3.once('3 should be called');

    ci.remove('a', cb2);
    cb2.setDisabledMessage('callback should not be invoked after removed');
    cb1.enable();
    ci.invoke('a');
    cb1.once('callback should still be invoked if not excatly the one being removed');

    ci.add('a', cb2);
    strictEqual(ci.hasEventListener('a'), true, '`has` should return true if has any callback');
    ci.removeAll('a');
    strictEqual(ci.hasEventListener('a'), false, '`has` should return false if all callbacks removed');
    cb1.setDisabledMessage('should not be called after all removed');
    cb2.setDisabledMessage('should not be called after all removed');
    ci.invoke('a');
});

test('remove self during invoking', function () {
    var ci = new cc._Test.CallbacksInvoker();
    var cb1 = Callback(function () {
        ci.remove('eve', cb1);
    }).enable();
    var cb2 = Callback().enable();

    ci.add('eve', cb1);
    ci.add('eve', cb2);
    ci.invoke('eve');
    cb2.once('it should be called correctly if previous callback deregistered itself');

    cb1.disable('should not call after removed');
    ci.invoke('eve');
});

test('remove self with target during invoking', function () {
    var ci = new cc._Test.CallbacksInvoker();
    var cb1 = Callback(function () {
        ci.remove('eve', cb1, target);
    }).enable();
    var cb2 = Callback().enable();
    var target = {};

    ci.add('eve', cb1, target);
    ci.add('eve', cb2, target);
    ci.invoke('eve');
    cb2.once('it should be called correctly if previous callback deregistered itself');

    cb1.disable('should not call after removed');
    ci.invoke('eve');
});

test('remove previous during invoking', function () {
    var ci = new cc._Test.CallbacksInvoker();
    var cb1 = Callback().enable();
    var cb2 = Callback(function () {
        ci.remove('eve', cb1);
    }).enable();

    ci.add('eve', cb1);
    ci.add('eve', cb2);
    ci.invoke('eve');

    strictEqual(ci.hasEventListener('eve', cb1), false, 'previous callback should be removed');
    strictEqual(ci.hasEventListener('eve', cb2), true, 'self callback should not be removed');
});

test('remove previous with target during invoking', function () {
    var ci = new cc._Test.CallbacksInvoker();
    var cb1 = Callback().enable();
    var cb2 = Callback(function () {
        ci.remove('eve', cb1, target);
    }).enable();
    var target = {};

    ci.add('eve', cb1, target);
    ci.add('eve', cb2, target);
    ci.invoke('eve');

    strictEqual(ci.hasEventListener('eve', cb1, target), false, 'previous callback should be removed');
    strictEqual(ci.hasEventListener('eve', cb2, target), true, 'self callback should not be removed');
});

test('remove last during invoking', function () {
    var ci = new cc._Test.CallbacksInvoker();
    var cb1 = Callback().enable();
    var cb2 = Callback(function () {
        ci.remove('eve', cb2);
    }).enable();

    ci.add('eve', cb1);
    ci.add('eve', cb2);
    ci.invoke('eve');

    strictEqual(ci.hasEventListener('eve', cb1), true, 'previous callback should not be removed');
    strictEqual(ci.hasEventListener('eve', cb2), false, 'self callback should be removed');
});

test('remove last with target during invoking', function () {
    var ci = new cc._Test.CallbacksInvoker();
    var cb1 = Callback().enable();
    var cb2 = Callback(function () {
        ci.remove('eve', cb2, target);
    }).enable();
    var target = {};
    ci.add('eve', cb1, target);
    ci.add('eve', cb2, target);
    ci.invoke('eve');

    strictEqual(ci.hasEventListener('eve', cb1, target), true, 'previous callback should not be removed');
    strictEqual(ci.hasEventListener('eve', cb2, target), false, 'self callback should be removed');
});

test('remove multiple callbacks during invoking', function () {
    var ci = new cc._Test.CallbacksInvoker();
    var cb1 = Callback().enable();
    var cb2 = Callback(function () {
        ci.remove('eve', cb1);
        ci.remove('eve', cb3, target);
    }).enable();
    var cb3 = Callback(function () {
        ci.remove('eve', cb2, target);
    }).enable();
    var target = {};
    ci.add('eve', cb1);
    ci.add('eve', cb1, target);
    ci.add('eve', cb2, target);
    ci.add('eve', cb3);
    ci.add('eve', cb3, target);
    ci.invoke('eve');

    cb1.expect(2, 'first callback should be invoked twice');
    cb2.expect(1, 'second callback should be invoked once');
    cb3.expect(1, 'third callback should be invoked once');
    strictEqual(ci.hasEventListener('eve', cb1, target), true, 'first callback with target should not be removed');
    strictEqual(ci.hasEventListener('eve', cb1), false, 'first callback should be removed');
    strictEqual(ci.hasEventListener('eve', cb2, target), false, 'second callback with target should be removed');
    strictEqual(ci.hasEventListener('eve', cb3, target), false, 'third callback with target should be removed');
});

test('remove all callbacks during invoking', function () {
    var ci = new cc._Test.CallbacksInvoker();
    var cb1 = Callback().enable();
    var cb2 = Callback(function () {
        ci.removeAll('eve');
    }).enable();
    var cb3 = Callback(function () {
        ci.remove('eve', cb2, target);
    }).enable();
    var target = {};
    ci.add('eve', cb1);
    ci.add('eve', cb1, target);
    ci.add('eve', cb2, target);
    ci.add('eve', cb3);
    ci.add('eve', cb3, target);
    ci.invoke('eve');

    cb1.expect(2, 'first callback should be invoked twice');
    cb2.expect(1, 'second callback should be invoked once');
    cb3.expect(0, 'third callback should never invoked');
    strictEqual(ci.hasEventListener('eve'), false, 'All callbacks should be removed');
});

test('remove and add again during invoking', function () {
    var target = new Object();
    var ci = new cc._Test.EventListeners();

    var cb1 = function () {
        ci.remove('eve', cb1, target);
        ci.add('eve', cb1, target);
    };

    ci.add('eve', cb1, target);
    ci.invoke(new cc.Event.EventCustom('eve'));

    strictEqual(ci.hasEventListener('eve', cb1, target), true, 'first callback should be added back');
});

test('remove twice and add again during invoking', function () {
   var target = new Object();
   var ci = new cc._Test.EventListeners();

   var cb1 = function () {
       ci.remove('eve', cb1, target);
       ci.remove('eve', cb1, target);
       ci.add('eve', cb1, target);
   };

   ci.add('eve', cb1, target);
   ci.invoke(new cc.Event.EventCustom('eve'));

   strictEqual(ci.hasEventListener('eve', cb1, target), true, 'first callback should be added back');
});

test('remove and check has during invoking', function () {
   var target = new Object();
   var ci = new cc._Test.EventListeners();

   var cb1 = function () {
       ci.remove('eve', cb1, target);
       strictEqual(ci.hasEventListener('eve', cb1, target), false, 'first callback should be removed');
   };

   ci.add('eve', cb1, target);
   ci.invoke(new cc.Event.EventCustom('eve'));
});

test('CallbacksInvoker support target', function () {
    var ci = new cc._Test.CallbacksInvoker();
    var cb1 = function () {
        cb1.count++;
        if (this.name)
            this.count++;
    };
    cb1.count = 0;
    var cb2 = Callback();
    var cb3 = Callback();

    var target1 = {
        name: 'CallbackTarget1',
        count: 0
    };
    var target2 = {
        name: 'CallbackTarget2',
        count: 0
    };

    ci.add('a', cb1);
    ci.add('a', cb1, target1);
    ci.add('a', cb1);
    ci.add('a', cb1, target2);
    ci.add('a', cb1, target2);
    ci.add('a', cb2, target2);
    ci.add('a', cb2, target1);
    ci.add('a', cb3);
    ci.add('a', cb3, target1);
    ci.add('b', cb1, target1);

    strictEqual(ci.hasEventListener('a', cb2), false, '`has` should return false if the callback without target not exists');
    strictEqual(ci.hasEventListener('a', cb2, target1), true, '`has` should return true if the callback with correct target exists');
    strictEqual(ci.hasEventListener('a', cb3), true, '`has` should return true if the callback without target exists');

    cb2.enable();
    cb3.enable();
    ci.invoke('a');
    strictEqual(cb1.count, 5, 'callback1 should be invoked five times');
    strictEqual(target1.count, 1, 'callback1 should be invoked one time with target1');
    strictEqual(target2.count, 2, 'callback2 should be invoked two times with target2');

    cb2.expect(2, 'callback2 should be called twice');
    cb3.expect(2, 'callback3 should be called twice');

    strictEqual(ci.hasEventListener('a', target1), false, '`has` should return false if the target is given');

    ci.remove('b', cb1);
    ci.remove('b', cb1, target2);
    strictEqual(ci.hasEventListener('b', cb1, target1), true, 'remove callback without the correct target should fail');
    ci.remove('b', cb1, target1);
    strictEqual(ci.hasEventListener('b', cb1, target1), false, 'remove callback with the correct callback and target should succeed');

    cb1.count = 0;
    target1.count = 0;
    target2.count = 0;
    cb2.calledCount = 0;
    cb3.calledCount = 0;
    ci.remove('a', cb1, target2);
    ci.remove('a', cb1, target1);
    ci.remove('a', cb2, target2);
    ci.remove('a', cb3, target2);
    ci.invoke('a');
    strictEqual(target1.count, 0, 'callback1 with target1 should not be invoked after remove');
    strictEqual(target2.count, 1, 'callback1 with target2 should be invoked only once after remove one');

    strictEqual(cb1.count, 3, 'callback1 should be invoked three times after removed two');
    cb2.expect(1, 'callback2 should be called once');
    cb3.expect(2, 'callback3 should be called twice');
});

test('CallbacksInvoker remove target', function () {
    var ci = new cc._Test.CallbacksInvoker();
    var cb1 = Callback();
    var cb2 = Callback();
    var cb3 = Callback();

    var target1 = {
        name: 'CallbackTarget1',
        count: 0
    };
    var target2 = {
        name: 'CallbackTarget2',
        count: 0
    };

    ci.add('a', cb1);
    ci.add('a', cb1, target1);
    ci.add('a', cb1);
    ci.add('a', cb1, target2);
    ci.add('a', cb1, target2);
    ci.add('a', cb2, target2);
    ci.add('a', cb2, target1);
    ci.add('a', cb3);
    ci.add('a', cb3, target1);

    cb1.enable();
    cb2.enable();
    cb3.enable();

    ci.invoke('a');
    cb1.expect(5, 'callback1 should be called five times');
    cb2.expect(2, 'callback2 should be called twice');
    cb3.expect(2, 'callback3 should be called twice');

    ci.removeAll(target1);

    cb1.calledCount = 0;
    cb2.calledCount = 0;
    cb3.calledCount = 0;

    ci.invoke('a');
    cb1.expect(4, 'removed one, callback1 should be called four times');
    cb2.expect(1, 'removed one, callback2 should be called once');
    cb3.expect(1, 'removed one, callback3 should be called once');

    ci.removeAll(target2);

    cb1.calledCount = 0;
    cb2.calledCount = 0;
    cb3.calledCount = 0;

    ci.invoke('a');
    cb1.expect(2, 'removed two, callback1 should be called twice');
    cb2.expect(0, 'removed one, callback2 should not be called');
    cb3.expect(1, 'callback3 should be called once');
});

test('event type conflict with object prototype', function () {
    var ci = new cc._Test.CallbacksInvoker();
    var cb1 = Callback(function () {
        ci.remove('toString', cb1);
    }).enable();
    var cb2 = Callback().enable();

    ci.add('toString', cb1);
    ci.add('toString', cb2);
    ci.invoke('toString');
    cb2.once('it should be called correctly if previous callback deregistered itself');

    cb1.disable('should not call after removed');
    ci.invoke('toString');
});

test('nest invoke', function () {
    var ci = new cc._Test.CallbacksInvoker();

    var actualSequence = [];
    var isParentLoop = true;

    var cb1 = function () {
        actualSequence.push(cb1);
        if (isParentLoop) {
            isParentLoop = false;
            ci.invoke('visit');
        }
    };
    var cb2 = function () {
        actualSequence.push(cb2);
    };

    ci.add('visit', cb1);
    ci.add('visit', cb2);
    ci.invoke('visit');

    deepEqual(actualSequence, [cb1, cb1, cb2, cb2], 'support nest invoke');
});

test('remove during nest invoke', function () {
    var ci = new cc._Test.CallbacksInvoker();

    var actualSequence = [];
    var isParentLoop = true;

    var cb1 = function () {
        actualSequence.push(cb1);
        if (isParentLoop) {
            isParentLoop = false;
            ci.invoke('visit');
        }
        else {
            ci.remove('visit', cb1);
            strictEqual(ci.hasEventListener('visit'), false, 'all callbacks removed');
        }
    };

    ci.add('visit', cb1);
    ci.invoke('visit');

    deepEqual(actualSequence, [cb1, cb1], 'invoke sequence');
});

test('remove many during nest invoke', function () {
    var ci = new cc._Test.CallbacksInvoker();

    var actualSequence = [];
    var isParentLoop = true;

    var cb1 = function () {
        actualSequence.push(cb1);
    };
    var cb2 = function () {
        actualSequence.push(cb2);
        if (isParentLoop) {
            isParentLoop = false;
            ci.invoke('visit');
        }
        else {
            ci.remove('visit', cb1);
            ci.remove('visit', cb2);
        }
    };
    var cb3 = function () {
        actualSequence.push(cb3);
    };

    ci.add('visit', cb1);
    ci.add('visit', cb2);
    ci.add('visit', cb3);
    ci.invoke('visit');

    deepEqual(actualSequence, [cb1, cb2, cb1, cb2, cb3, cb3], 'invoke sequence');
});
