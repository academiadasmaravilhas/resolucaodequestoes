using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Atenção: o arquivo executável (.exe) contendo o seu programa compilado assim como a pasta de Questões podem ser encontrados na pasta /bin
// Confira a pasta /bin

namespace Trabalho_2C
{
    public partial class FormPrincipal : Form
    {
        // Questão atual
        Questão questao;
        // diretorio atual
        string diretorioAtual;
        private int indiceAtual = -1; // Adicione essa variável como campo de classe
        public FormPrincipal()
        {
            // Não precisa se preocupar aqui -----------------------

            // Inicializa os componentes do formulário (método interno que "arruma" os controles na tela de acordo com o que foi desenhado no Designer
            InitializeComponent();

            // Recebe o diretório em que o programa propriamente (arquivo .exe) está sendo executado
            diretorioAtual = Directory.GetCurrentDirectory();

            // Dado o diretório atual, cria um caminho que aponta para a pasta Questões
            diretorioAtual += @"\..\..\..\Questões\";

            // Método que retorna um vetor de strings contendo o diretório de todas as pastas que estão dentro da pasta Questões
            string[] diretorios = Directory.GetDirectories(diretorioAtual);

            // Percorre cada string dentro do vetor de strings
            for (int i = 0; i < diretorios.Length; i++)
            {
                // Para cada caminho, quero obter apenas o nome da disciplina
                string nomeDaDisciplina = diretorios[i].Remove(0, diretorioAtual.Length);
                // Vou adicionar o nome da disciplina aos itens da comboBox1
                cmbDisciplinas.Items.Add(nomeDaDisciplina);
            }
            // Não precisa se preocupar aqui -----------------------

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Não precisa se preocupar aqui -----------------------
            // Define o diretório da matéria selecionada na comboBox1
            string diretorioMateria = diretorioAtual + cmbDisciplinas.Text;
            // Obtém um vetor de strings contendo o diretório para todos os arquivos
            string[] arquivos = Directory.GetFiles(diretorioMateria, "*.txt");
            // Não precisa se preocupar aqui -----------------------

            // Caso não haja nenhum arquivo no diretorio da matéria selecionada, mostrar uma mensagem ao usuário
            if (arquivos.Length == 0) MessageBox.Show("Não há questões na pasta selecionada!");
            // Caso haja algum arquivo no diretorio da matéria selecionada, o enunciado do primeiro exercício é carregado como exemplo
            else
            {
                // Cria objeto da classe Questão
                questao = new Questão();

                // Cria um leitor para ler as informações da questão
                StreamReader leitor = new StreamReader(arquivos[0]);

                // Preenche os atributos da questão com as informações lidas do arquivo
                questao.enunciado = leitor.ReadLine();
                questao.altA = leitor.ReadLine();
                questao.altB = leitor.ReadLine();
                questao.altC = leitor.ReadLine();
                questao.altD = leitor.ReadLine();
                questao.altE = leitor.ReadLine();

                // Preenche os controles do formulário usando o objeto questão
                txtEnunciado.Text = questao.enunciado;
                rdbA.Text = questao.altA;
                rdbB.Text = questao.altB;
                rdbD.Text = questao.altC;
                rdbC.Text = questao.altD;
                rdbE.Text = questao.altE;

                leitor.Close();
            }
        }

        private void btnProximaPergunta_Click(object sender, EventArgs e)
        {

            if (cmbDisciplinas.SelectedIndex >= 0)
            {

                string diretorioMateria = diretorioAtual + cmbDisciplinas.Text;


                string[] arquivos = Directory.GetFiles(diretorioMateria, "*.txt");


                if (arquivos.Length > 0)
                {

                    if (indiceAtual == arquivos.Length - 1)
                    {
                        MessageBox.Show("Você chegou na última pergunta.");
                    }
                    else
                    {

                        indiceAtual++;


                        string proximoArquivo = arquivos[indiceAtual];


                        using (StreamReader leitor = new StreamReader(proximoArquivo))
                        {

                            Questão proximaQuestao = new Questão();

                            proximaQuestao.enunciado = leitor.ReadLine();
                            proximaQuestao.altA = leitor.ReadLine();
                            proximaQuestao.altB = leitor.ReadLine();
                            proximaQuestao.altC = leitor.ReadLine();
                            proximaQuestao.altD = leitor.ReadLine();
                            proximaQuestao.altE = leitor.ReadLine();

                            questao = proximaQuestao;

                            txtEnunciado.Text = questao.enunciado;
                            rdbA.Text = questao.altA;
                            rdbB.Text = questao.altB;
                            rdbC.Text = questao.altC;
                            rdbD.Text = questao.altD;
                            rdbE.Text = questao.altE;


 
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Não há questões na pasta selecionada!");
                }
            }
        }

        private void btnResponder_Click(object sender, EventArgs e)
        {
            if (cmbDisciplinas.SelectedIndex >= 0)
            {

                string diretorioMateria = diretorioAtual + cmbDisciplinas.Text;


                string[] arquivos = Directory.GetFiles(diretorioMateria, "*.txt");

                if (arquivos.Length > 0)
                {

                    if (rdbA.Checked || rdbB.Checked || rdbC.Checked || rdbD.Checked || rdbE.Checked)
                    {

                        if (indiceAtual >= 0 && indiceAtual < arquivos.Length)
                        {

                            string arquivoQuestaoAtual = arquivos[indiceAtual];


                            using (StreamReader leitor = new StreamReader(arquivoQuestaoAtual))
                            {

                                string enunciado = leitor.ReadLine();
                                string altA = leitor.ReadLine();
                                string altB = leitor.ReadLine();
                                string altC = leitor.ReadLine();
                                string altD = leitor.ReadLine();
                                string altE = leitor.ReadLine();
                                string resolucao = leitor.ReadLine();
                                int numeroAcertos = int.Parse(leitor.ReadLine());
                                int numeroErros = int.Parse(leitor.ReadLine());
                                string respostaCorreta = leitor.ReadLine();

                                string respostaSelecionada = "";
                                if (rdbA.Checked)
                                {
                                    respostaSelecionada = "a";
                                }
                                else if (rdbB.Checked)
                                {
                                    respostaSelecionada = "b";
                                }
                                else if (rdbC.Checked)
                                {
                                    respostaSelecionada = "c";
                                }
                                else if (rdbD.Checked)
                                {
                                    respostaSelecionada = "d";
                                }
                                else if (rdbE.Checked)
                                {
                                    respostaSelecionada = "e";
                                }

                                if (respostaSelecionada.Equals(respostaCorreta, StringComparison.OrdinalIgnoreCase))
                                {
                                    numeroAcertos++;
                                    MessageBox.Show("Resposta correta!");
                                }
                                else
                                {
                                    numeroErros++;
                                    MessageBox.Show("Resposta incorreta!");
                                }

                                leitor.Close();

                                using (StreamWriter escritor = new StreamWriter(arquivoQuestaoAtual))
                                {
                                    escritor.WriteLine(enunciado);
                                    escritor.WriteLine(altA);
                                    escritor.WriteLine(altB);
                                    escritor.WriteLine(altC);
                                    escritor.WriteLine(altD);
                                    escritor.WriteLine(altE);
                                    escritor.WriteLine(resolucao);
                                    escritor.WriteLine(numeroAcertos);
                                    escritor.WriteLine(numeroErros);
                                    escritor.WriteLine(respostaCorreta);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selecione uma questão válida!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecione uma opção de resposta!");
                    }
                }
            }
        }

    }
        }
        

        
    

