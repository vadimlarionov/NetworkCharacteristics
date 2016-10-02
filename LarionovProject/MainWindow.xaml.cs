using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LarionovProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string DOUBLE_TO_STRING_FORMAT = "0.000000";

        private double lambda;
        private double kInput;

        private double muStartNode;
        private double kStartNode;

        private double muMiddleNode;
        private double kMiddleNode;

        private double muEndNode;
        private double kEndNode;

        public MainWindow()
        {
            InitializeComponent();
            Calculate();
        }

        private void Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Menu_AboutProgram_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Программа выполнена в рамках курсовой работы")
                .AppendLine("по предмету \"Аналитические модели АСОИиУ\"")
                .AppendLine()
                .AppendLine("Автор: Ларионов Вадим Станиславович")
                .AppendLine("Группа: ИУ5-18");
            MessageBox.Show(builder.ToString(), "О программе");
        }

        private void TextBoxValidateFloat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0)
            {
                char ch = e.Text[0];
                e.Handled = !(Char.IsDigit(ch) || ch == ',' || ch == '.');
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        private void Calculate()
        {
            try
            {
                StatusBarMessage.Text = "Выполняются вычисления";
                if (!InitializeVariables())
                {
                    return;
                }

                double loadFactor_StartNode = NetworkCharacteristics.LoadFactor(lambda, muStartNode);
                StatusBarMessage.Text = loadFactor_StartNode.ToString();
                if (loadFactor_StartNode > 1)
                {
                    throw new System.InvalidOperationException("Load factor for start node = " + loadFactor_StartNode + " > 1");
                }
                double Q_StartNode = NetworkCharacteristics.CalculateQ(loadFactor_StartNode, 1 / kInput, 1 / kStartNode);
                double L_StartNode = NetworkCharacteristics.CalculateL(Q_StartNode, loadFactor_StartNode);
                double W_StartNode = NetworkCharacteristics.CalculateW(Q_StartNode, lambda);
                double T_StartNode = NetworkCharacteristics.CalculateT(L_StartNode, lambda);

                double loadFactor_MiddleNode = NetworkCharacteristics.LoadFactor(lambda, muMiddleNode);
                if (loadFactor_StartNode > 1)
                {
                    throw new System.InvalidOperationException("Load factor for middle node = " + loadFactor_StartNode + " > 1");
                }
                double Q_MiddleNode = NetworkCharacteristics.CalculateQ(loadFactor_MiddleNode, 1 / kInput, 1 / kMiddleNode);
                double L_MiddleNode = NetworkCharacteristics.CalculateL(Q_MiddleNode, loadFactor_MiddleNode);
                double W_MiddleNode = NetworkCharacteristics.CalculateW(Q_MiddleNode, lambda);
                double T_MiddleNode = NetworkCharacteristics.CalculateT(L_MiddleNode, lambda);


                double loadFactor_EndNode = NetworkCharacteristics.LoadFactor(lambda, muEndNode);
                if (loadFactor_StartNode > 1)
                {
                    throw new System.InvalidOperationException("Load factor for middle node = " + loadFactor_StartNode + " > 1");
                }

                double Q_EndNode = NetworkCharacteristics.CalculateQ(loadFactor_EndNode, 1 / kInput, 1 / kEndNode);
                double L_EndNode = NetworkCharacteristics.CalculateL(Q_EndNode, loadFactor_EndNode);
                double W_EndNode = NetworkCharacteristics.CalculateW(Q_EndNode, lambda);
                double T_EndNode = NetworkCharacteristics.CalculateT(L_EndNode, lambda);

                Q_Result_TextBox.Text = (Q_StartNode + Q_MiddleNode + Q_EndNode).ToString(DOUBLE_TO_STRING_FORMAT);
                L_Result_TextBox.Text = (L_StartNode + L_MiddleNode + L_EndNode).ToString(DOUBLE_TO_STRING_FORMAT);
                W_Result_TextBox.Text = (W_StartNode + W_MiddleNode + W_EndNode).ToString(DOUBLE_TO_STRING_FORMAT);
                T_Result_TextBox.Text = (T_StartNode + T_MiddleNode + T_EndNode).ToString(DOUBLE_TO_STRING_FORMAT);
                StatusBarMessage.Text = "Готово";
            }
            catch (Exception exception)
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Что-то пошло не так. Проверьте входные данные и попробуйте ещё раз.")
                    .AppendLine()
                    .Append("Message: ")
                    .Append(exception.Message.ToString());
                MessageBox.Show(builder.ToString(), "Ошибка выполнения");
                return;
            }
        }

        private bool InitializeVariables()
        {
            if (!Double.TryParse(Lambda_TextBox.Text.ToString().Replace('.', ','), out lambda))
            {
                StatusBarMessage.Text = "Некорректный параметр λ входного потока";
                return false;
            }

            if (!Double.TryParse(K_Input_TextBox.Text.ToString().Replace('.',','), out kInput))
            {
                StatusBarMessage.Text = "Некорректный параметр k входного потока";
                return false;
            }

            if (!Double.TryParse(Mu_StartNode_TextBox.Text.ToString().Replace('.', ','), out muStartNode))
            {
                StatusBarMessage.Text = "Некорректный параметр μ начального узла";
                return false;
            }

            if (!Double.TryParse(K_StartNode_TextBox.Text.ToString().Replace('.', ','), out kStartNode))
            {
                StatusBarMessage.Text = "Некорректный параметр k начального узла";
                return false;
            }

            if (!Double.TryParse(Mu_MiddleNode_TextBox.Text.ToString().Replace('.', ','), out muMiddleNode))
            {
                StatusBarMessage.Text = "Некорректный параметр μ промежуточного узла";
                return false;
            }

            if (!Double.TryParse(K_MiddleNode_TextBox.Text.ToString().Replace('.', ','), out kMiddleNode))
            {
                StatusBarMessage.Text = "Некорректный параметр k промежуточного узла";
                return false;
            }

            if (!Double.TryParse(Mu_EndNode_TextBox.Text.ToString().Replace('.', ','), out muEndNode))
            {
                StatusBarMessage.Text = "Некорректный параметр μ конечного узла";
                return false;
            }

            if (!Double.TryParse(K_EndNode_TextBox.Text.ToString().Replace('.', ','), out kEndNode))
            {
                StatusBarMessage.Text = "Некорректный параметр k конечного узла";
                return false;
            }

            return true;
        }

    }
}
