<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Discuss.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        #discuss {
            margin: 10px;
            font-family:'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', 'DejaVu Sans Condensed', sans-serif;
            font-size: 14px;

        }
            #discuss .time {
                margin-top: 10px;
                text-align: right;
                font-size: 12px;
                color: #666;
            }

            #discuss img {
                padding: 0 10px 10px 0;
            }

            #discuss td {
                vertical-align: top;
            }

            #discuss hr {
                margin-bottom: 30px;
                color: #333;
            }

        #Content {
            margin: 0 10px 10px 10px;
        }
    </style>
    <link href="style/normalize.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
    <script src="script/moment.js"></script>
    <script src="script/livestamp.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="OriginalReferrer" Visible="false" runat="server" Text=""></asp:Label>
            <asp:Label ID="OriginalTopicId" Visible="false" runat="server" Text=""></asp:Label>
            <asp:Label ID="Output" Visible="false" runat="server" Text=""></asp:Label>
            <table id="discuss">
                <asp:Literal ID="Messages" runat="server"></asp:Literal>
            </table>
            <asp:TextBox ID="Content" TextMode="MultiLine" Rows="5" MaxLength="500" Width="350" runat="server"></asp:TextBox>
            <asp:Button ID="Submit" runat="server" Text="Submit" /><br />
        </div>
    </form>
    <script>
        window.onload = toBottom;

        function toBottom() {
            //alert("Scrolling to bottom ...");
            window.scrollTo(0, document.body.scrollHeight);
        }

    </script>
</body>
</html>