/// <reference path="jquery-1.6.4-vsdoc.js" />
/// <reference path="jquery.signalR-2.1.1.js" />
/// <reference path="rx.lite.js" />

$(function () {

    $("#start").click(function () {
        var hubConnection = $.hubConnection();

        var tickerHub = hubConnection.createHubProxy("tickerHub");

        var tickerObservable = Rx.Observable.fromEventPattern(
            function addHandler(h) { tickerHub.on("tick", h); },
            function removeHandler(h) { tickerHub.off("tick", h); }
        );

        var subscription = tickerObservable.subscribe(function (nextVal) {
            $("#tickerList").append("<li>" + nextVal + "</li>")
        });

        $("#stop").click(function () {
            subscription.dispose();
        });

        hubConnection.start()
                     .done(function () {
                         tickerHub.invoke("register");
                     });
    });
});