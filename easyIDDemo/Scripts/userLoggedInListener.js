var eventMethod = window.addEventListener ? "addEventListener" : "attachEvent";
var eventer = window[eventMethod];
var messageEvent = eventMethod == "attachEvent" ? "onmessage" : "message";

// Listen to message from child window and send the user to the desired target URL. 
// For this demo, we stay on the home page, but you could certainly add some more
// refined logic for taking the user to a better place.
eventer(messageEvent, function (e) {
    if (e && e.data && e.origin === document.location.origin && e.data.userLoggedIn) {
        window.location = '/';
    }
}, false);
