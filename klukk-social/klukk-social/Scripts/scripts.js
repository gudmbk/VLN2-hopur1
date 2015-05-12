$(document).ready(function () {
    $(".friend-req").click(function() {
        var userToAdd = $(this).val();
        var json = {Id: 0, ToUserId: userToAdd, FromUserId: ""}
        var toHide = $(this);
        $.ajax({
                type: "POST",
                url: "/User/SendFriendRequest",
                traditional: true,
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(json),
            success: function () { toHide.hide(); },
            error: function(data) { console.log(data) }
        });
    });

    $(".accept-friend").click(function () {
        var fromUser = $(this).val();
        var json = { Id: 0, ToUserId: "", FromUserId: fromUser }
        var toHide = $(this);
        $.ajax({
            type: "POST",
            url: "/User/AcceptFriendRequest",
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(json),
            success: function() { toHide.parent().hide()},
            error: function (data) { console.log(data) }
        });
    });   
    
    $('.like-button').hover(
	// Handles the mouseover
	function () {
		$(this).andSelf().addClass('btn-warning');
	},
	// Handles the mouseout
	function () {
		$(this).andSelf().removeClass('btn-warning');
	});
});

$(function () {
    var likeClient = $.connection.likeHub;
    likeClient.client.updateLikeCount = function (like) {
        var counter = $(".like-count");
        $(counter).fadeOut(function () {
            $(this).text(like);
            $(this).fadeIn();
        });
    };
    $(".like-button").on("click", function () {
        var code = $(this).attr("data-id");
        var user = $(this).attr("data-user");
        likeClient.server.like(code, user);
    });

    $.connection.hub.start();

});