// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Details View
function showTab(tabId) {
    // Hide all tab panes
    $('.tab-pane').removeClass('show active');

    // Show the clicked tab pane
    $('#' + tabId).addClass('show active');

    // Update the active link class
    $('#departmentTabs .nav-link').removeClass('active');
    $('[aria-controls="' + tabId + '"]').addClass('active');
}
