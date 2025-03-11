import React, { useState } from "react";
import { View, Text, StyleSheet, TouchableOpacity, ScrollView } from "react-native";
import CustomButton from "../components/CustomButton";

const atividades = [
  { nome: "1️⃣ Completar o cadastro", descricao: "Para completar o cadastro pessoal, preencha seus dados pessoais como nome, CPF, etc." },
  { nome: "2️⃣ Endereço de residência", descricao: "Insira o seu endereço completo para garantir que os dados sejam precisos." },
  { nome: "3️⃣ Endereço de preferência", descricao: "Defina o endereço onde você prefere receber o atendimento." },
  { nome: "4️⃣ Dia de preferência", descricao: "Escolha o dia da semana em que você prefere ser atendido." },
  { nome: "5️⃣ Turno de preferência", descricao: "Escolha o turno de preferência: manhã, tarde ou noite." },
  { nome: "6️⃣ Responder um feedback", descricao: "Após um atendimento, você será solicitado a responder um feedback sobre a experiência." },
  { nome: "7️⃣ Aceitar consulta sugerida", descricao: "Considere realizar uma consulta que foi sugerida para melhorar seu tratamento." },
  { nome: "8️⃣ Realizar uma consulta", descricao: "Marque uma consulta online com a nossa equipe para um atendimento remoto." },
  { nome: "9️⃣ Assistir vídeos preventivos", descricao: "Assista três vídeos educativos para melhorar seu conhecimento sobre cuidados de saúde." },
  { nome: "🔟 Não recusar sugestão", descricao: "É importante aceitar as sugestões de atendimento para manter o seu progresso." },
  { nome: "1️⃣1️⃣ Três sugestões aceitas", descricao: "Certifique-se de aceitar pelo menos três sugestões durante o ano." },
  { nome: "1️⃣2️⃣ Indicar a OdontoPrev", descricao: "Compartilhe os benefícios da OdontoPrev com seus amigos e familiares." },
];

//const AtividadesProgramaBeneficioClienteScreen = () => {
  const AtividadesProgramaBeneficioClienteScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
    
  const [expandedIndex, setExpandedIndex] = useState<number | null>(null);
  

  const toggleExpand = (index: number) => {
    if (expandedIndex === index) {
      setExpandedIndex(null); // Recolhe se já estiver expandido
    } else {
      setExpandedIndex(index); // Expande a seção
    }
  };

  return (
    <ScrollView style={styles.container}>

      <Text style={styles.title}>Sobre as atividades</Text>

      <Text style={styles.description}>Para obter mais informações sobre cada atividade, basta clicar em cada card. As informações serão exibidas de forma clara e objetiva, facilitando o seu acompanhamento.</Text>

      {atividades.map((atividade, index) => (
        <View key={index} style={styles.card}>
          
          <TouchableOpacity onPress={() => toggleExpand(index)}>
            <Text style={styles.cardTitle}>{atividade.nome}</Text>
          </TouchableOpacity>

          {expandedIndex === index && (
            <View style={styles.cardDetails}>
              <Text style={styles.cardDescription}>{atividade.descricao}</Text>
            </View>
          )}
        </View>
      ))}

      <View style={styles.buttonContainer}>
        <CustomButton title="Conheça as Recompensas" onPress={() => navigation.navigate("ProgramaBeneficioCliente")} width={"100%"} textColor="#fff" />
      </View>

      <View style={styles.buttonContainer}>
        <CustomButton title="Home" onPress={() => navigation.navigate("SessaoRestritaCliente")} width={"100%"} textColor="#fff" backgroundColor="#f28705"/>
      </View>

    </ScrollView>
  );
};

const styles = StyleSheet.create({

  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
    textAlign: 'left',
    color: '#024059',
  },
  description: {
    fontSize: 16,
    marginBottom: 20,
    textAlign: 'left',
    color: '#333',
  },
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: "#f8f8f8",
  },
  card: {
    backgroundColor: "#fff",
    marginBottom: 10,
    padding: 15,
    borderRadius: 10,
    shadowColor: "#000",
    shadowOpacity: 0.1,
    shadowOffset: { width: 0, height: 2 },
    elevation: 3,
  },
  cardTitle: {
    fontSize: 18,
    fontWeight: "bold",
    color: "#333",
  },
  cardDetails: {
    marginTop: 10,
  },
  cardDescription: {
    fontSize: 14,
    color: "#666",
  },
  buttonContainer: {
    width: "100%",
    alignItems: "center",
    marginBottom:0,
    marginTop:10,
  },
});

export default AtividadesProgramaBeneficioClienteScreen;
