// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your jQuery/Javascript code.

$(document).ready(function () {
    //this sets the active link , 
    //the selected route/page will be selected
    var timeout = $('#TimeOut').val(); // '@AppSettings.SessionTimeOutMinutes';
    var url = window.location.href;
   
    // for sidebar menu entirely but not cover treeview
    $('ul.nav-sidebar a').filter(function () {
        return this.href == url;
    }).addClass('active');

    // for treeview
    $('ul.nav-treeview a').filter(function () {
        return this.href == url;
    }).parentsUntil(".nav-sidebar > .nav-treeview").addClass('menu-open').prev('a').addClass('active');

    $.idleHands({
        applicationId: 'Proteus',
        heartRate: 15,
        inactivityDialogDuration: 45,
        inactivityLogoutUrl: '/Identity/Account/Logout',
        maxInactivitySeconds: timeout * 60//get this from the session setting
    });

});