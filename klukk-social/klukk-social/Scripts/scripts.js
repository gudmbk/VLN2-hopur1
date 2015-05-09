$(document).ready(function () {
    alert("sdad");
    $(".add-friend").click(function() {
        var userId = $(this).val();
        var jsonUserId = { userId: userId };
        $.ajax({
            type: "POST",
            url: "/MovieApp/SendFriendRequest",
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: jsonUserId,
            success: function(data) { console.log(data) },
            error: function(data) { console.log(data) }
        });
    });
});