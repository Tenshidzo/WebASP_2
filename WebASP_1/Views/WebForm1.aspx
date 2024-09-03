<%@ Page Language="C#" Debug="true" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebASP_1.Views.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Игра с матрицей кнопок</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        function generateMatrix(size) {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/GenerateMatrix",
                data: JSON.stringify({ matrixSize: size }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#MatrixPanel").html(response.d.MatrixHtml);
                    $("#ScoreLabel").text("Счёт: " + response.d.Score);
                },
                failure: function (response) {
                    alert("Ошибка при генерации матрицы");
                }
            });
        }

        function buttonClick(buttonId) {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/ButtonClick",
                data: JSON.stringify({ buttonId: buttonId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#MatrixPanel").html(response.d.MatrixHtml);
                    $("#ScoreLabel").text("Счёт: " + response.d.Score);
                },
                failure: function (response) {
                    alert("Ошибка при обработке нажатия кнопки");
                }
            });
        }

        function resetGame() {
            $.ajax({
                type: "POST",
                url: "WebForm1.aspx/ResetGame",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#MatrixPanel").html("");
                    $("#ScoreLabel").text("Счёт: 0");
                },
                failure: function (response) {
                    alert("Ошибка при сбросе игры");
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="ScoreLabel" runat="server" Text="Счёт: 0" Font-Bold="True"></asp:Label>
            <br /><br />
            <button type="button" onclick="generateMatrix(3)">3x3</button>
            <button type="button" onclick="generateMatrix(4)">4x4</button>
            <button type="button" onclick="generateMatrix(5)">5x5</button>
            <br /><br />
            <div id="MatrixPanel"></div>
            <br />
            <button type="button" onclick="resetGame()">Reset</button>

            <!-- Скрытые поля для хранения состояния -->
            <asp:HiddenField ID="MatrixSizeHidden" runat="server" />
            <asp:HiddenField ID="MatrixDataHidden" runat="server" />
            <asp:HiddenField ID="FirstButtonValueHidden" runat="server" />
            <asp:HiddenField ID="FirstButtonIDHidden" runat="server" />
        </div>
    </form>
</body>
</html>
