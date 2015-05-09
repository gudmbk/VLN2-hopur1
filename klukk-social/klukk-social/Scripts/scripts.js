$(document).ready(function () {
    
    $(".add-friend").click(function() {
        var userToAdd = $(this).val();
        var json = {Id: 0, ToUserId: userToAdd, FromUserId: ""}
        alert("Virkar en er hægt að skrifa oftar en einu sinni í töflu svo ekki spamma takkann :)");
        $.ajax({
                type: "POST",
                url: "/User/SendFriendRequest",
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(json),
            success: function (data) { console.log(data) },
            error: function(data) { console.log(data) }
        });
    });
});