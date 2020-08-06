// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your jQuery/Javascript code.


$(document).ready(function () {
    //this sets the active link , 
    //the selected route/page will be selected

    var url = window.location.href;
    
    // for sidebar menu entirely but not cover treeview
    $('ul.nav-sidebar a').filter(function () {
        return this.href == url;
    }).addClass('active');

    // for treeview
    $('ul.nav-treeview a').filter(function () {
        return this.href == url;
    }).parentsUntil(".nav-sidebar > .nav-treeview").addClass('menu-open').prev('a').addClass('active');

    //for the session warning dialog box from https://www.jqueryscript.net/other/session-expiration-idle-hands.html
    var timeout = $('#TimeOut').val(); // '@AppSettings.SessionTimeOutMinutes';
    $.idleHands({
        applicationId: 'Proteus',
        heartRate: 15, //how often to check in seconds
        inactivityDialogDuration: 45, //seconds 
        inactivityLogoutUrl: '/Identity/Account/Logout',
        maxInactivitySeconds: timeout * 60//get this from the session setting convert to seconds
    });

});