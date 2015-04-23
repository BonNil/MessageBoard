<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MessageBoard.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #BulletedList1 {
            text-align: left;
            color: #333333;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <script src="Scripts/jquery-1.10.2.min.js"></script>

<script type="text/javascript">

    function getMessages() {
        $.getJSON("api/messages",
            function (data) {
                $('#BulletedList1').empty(); 

                $.each(data, function (key, val) {
                    var row = '<li>' + val.PostTime + ': ' + val.TextMessage + '</li>';
                    $('#BulletedList1').append(row);
                });
            });
    }
    $(document).ready(function () {
        getMessages();
        setInterval(getMessages, 2500);
    });

</script>
    <ul class="List" id="BulletedList1">
    </ul>
    </div>
    </form>
</body>
</html>
