﻿$(document).ready(function () {
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

    $(".add-star").click(function () {
        var itemToStar = $(this).val();
        var toHide = $(this);
        $.ajax({
            type: "POST",
            url: "/Post/AddLike",
            traditional: true,
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(itemToStar),
            success: function () { alert("SirLikesAlot") },
            error: function (data) { console.log(data) }
        });
    });







});
