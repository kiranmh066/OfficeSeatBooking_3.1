// Get the event time from the server (this can be done using AJAX)
//var Shift_Time = new Date('');

//// Calculate the reminder time
//var reminderTime = new Date(Shift_Time.getTime() - 15 * 60 * 1000);

//// Set a timer to check the current time every minute
//setInterval(function () {
//    var currentTime = new Date();

//    // If the current time is equal to or greater than the reminder time, show a notification
//    if (currentTime >= reminderTime) {
//        var notification = new Notification('Reminder', {
//            body: 'Your event is starting in 15 minutes!'
//        });
//    }
//}, 60000);
//$(document).ready(function () {
//    var now = new Date();
//    var millisTill1 = new Date(now.getFullYear(), now.getMonth(), now.getDate(), 13, 0, 0, 0) - now;
//    if (millisTill1 < 0) {
//        millisTill1 += 86400000;
//    }
//    setTimeout(function () {
//        alert("It's 1 o'clock!");
//    }, millisTill1);
//});
function setTimer() {
setInterval(function () {
    var now = new Date();
    if (now.getHours() == 15 && now.getMinutes() == 59) {
        showMessage();
    }
}, 1000); // Check every second
}
function showMessage() {
    alert("Hello! It's 1 o'clock.");
}

