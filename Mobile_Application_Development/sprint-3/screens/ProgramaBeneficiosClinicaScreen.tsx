import React, { useEffect, useState } from "react";
import { View, Text, StyleSheet, ScrollView, Dimensions, Image, TouchableOpacity } from "react-native";
import CustomButton from "../components/CustomButton";
import { getAuth } from "firebase/auth";
import { getFirestore, doc, getDoc } from "firebase/firestore";
import { useNavigation } from '@react-navigation/native';

const { width } = Dimensions.get("window");
const images = [
  require("../assets/ProgramaBeneficios/premio-um.jpg"),
  require("../assets/ProgramaBeneficios/premio-dois.jpg"),
  require("../assets/ProgramaBeneficios/premio-tres.jpg"),
];

const atividades = [
  { nome: "Completar o cadastro", tabela: "t_dados_cadastrais_clinica" },
  { nome: "Cadastrar endereço da clínica", tabela: "t_endereco_clinica" },
  { nome: "Cadastrar endereço de preferência", tabela: "t_endereco_preferencia_cliente" },
  { nome: "Cadastrar dia de preferência", tabela: "t_dia_preferencia_clinicas" },
  { nome: "Cadastrar turno de disponiblidade", tabela: "t_turno_preferencia_cliente" },
  { nome: "Responder um feedback", tabela: "t_resposta_feedback_clinica" },
  { nome: "Realizar uma consulta agendada", tabela: "t_consulta_realizada_clinica" },
  { nome: "Realizar uma consulta online", tabela: "t_consulta_realizada_online" },
  { nome: "Assistir três vídeos preventivos", tabela: "t_videos_preventivos" },
  { nome: "Não recusar nenhuma sugestão", tabela: "t_sugestao_consulta_clinica" },
  { nome: "Ter dez sugestões aceitas no ano", tabela: "t_sugestao_consulta" },
  { nome: "Indicar a OdontoPrev para três amigos", tabela: "t_indicacao_odontoPrev_cliente" },
  { nome: "Enviar conteúdos preventivos", tabela: "t_conteudo_preventivo" },
  { nome: "Cadastrar um médico", tabela: "t_dados_cadastrais_medicos" },
];

const ProgramaBeneficiosClinicaScreen = () => {
  const [nivel, setNivel] = useState(0);
  const [pontos, setPontos] = useState(0);
  const [atividadesConcluidas, setAtividadesConcluidas] = useState<string[]>([]);
  const [atividadesPendentes, setAtividadesPendentes] = useState<string[]>([]);
  const [activeIndex, setActiveIndex] = useState(0);
  const auth = getAuth();
  const db = getFirestore();
  const navigation = useNavigation();

  useEffect(() => {
    const fetchUserProgress = async () => {
      const user = auth.currentUser;
      if (!user) return;

      let completed = [];
      let pending = [];

      // Itera sobre as atividades e consulta as tabelas dinamicamente
      for (const atividade of atividades) {
        const docRef = doc(db, atividade.tabela, user.uid);
        const docSnap = await getDoc(docRef);

        if (docSnap.exists()) {
          completed.push(atividade.nome); // Se existir, a atividade foi completada
        } else {
          pending.push(atividade.nome); // Se não, a atividade está pendente
        }
      }

      setAtividadesConcluidas(completed);
      setAtividadesPendentes(pending);
      setNivel(completed.length);
      setPontos(completed.length * 10); // Cada atividade concluída vale 10 pontos
    };

    fetchUserProgress();
  }, []);

  useEffect(() => {
    const interval = setInterval(() => {
      const nextIndex = (activeIndex + 1) % 2; // Alterna entre as atividades concluídas e pendentes
      setActiveIndex(nextIndex);
    }, 5000);

    return () => clearInterval(interval);
  }, [activeIndex]);

  return (
    <View style={styles.container}>
      {/* Carrossel de Prêmios */}
      <ScrollView horizontal pagingEnabled showsHorizontalScrollIndicator={false} style={styles.carousel}>
        {images.map((img, index) => (
          <Image key={index} source={img} style={styles.image} />
        ))}
      </ScrollView>

      {/* Nível do Cliente */}
      <View style={styles.card}>
        <Text style={styles.title}>Seu nível neste momento: {nivel}</Text>
        <Text>Pontos adquiridos até agora: {pontos}</Text>
      </View>

      {/* Carrossel de Atividades */}
      <View style={styles.card}>
        <ScrollView horizontal pagingEnabled showsHorizontalScrollIndicator={false} style={styles.activitiesCarousel}>
          <View style={styles.activitiesCard}>
            <Text style={styles.title}>Atividades Concluídas</Text>
            {atividadesConcluidas.length > 0 ? (
              atividadesConcluidas.map((item, index) => (
                <Text key={index} style={styles.itemText}>✅ {item}</Text>
              ))
            ) : (
              <Text style={styles.itemText}>Nenhuma atividade concluída ainda.</Text>
            )}
          </View>

          <View style={styles.activitiesCard}>
            <Text style={styles.title}>Atividades Pendentes</Text>
            {atividadesPendentes.length > 0 ? (
              atividadesPendentes.map((item, index) => (
                <Text key={index} style={styles.itemText}>❌ {item}</Text>
              ))
            ) : (
              <Text style={styles.itemText}>Nenhuma atividade pendente no momento.</Text>
            )}
          </View>
        </ScrollView>

        {/* Bolinhas para navegação */}
        <View style={styles.dotsContainer}>
          {['Concluídas', 'Pendentes'].map((item, index) => (
            <TouchableOpacity
              key={index}
              style={[styles.dot, activeIndex === index && styles.activeDot]}
              onPress={() => setActiveIndex(index)}
            />
          ))}
        </View>
      </View>

      <CustomButton title="conheça as atividades" onPress={() => navigation.navigate("AtividadesProgramaBeneficioCliente")} width={'100%'} textColor='#fff'/>

    </View>
    
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignItems: "center",
    padding: 20,
  },
  carousel: {
    height: 200,
    marginBottom: 20,
  },
  image: {
    width: width * 0.9,
    height: 200,
  },
  card: {
    width: "100%",
    backgroundColor: "#fff",
    padding: 20,
    borderRadius: 5,
    marginBottom: 15,
    alignItems: "flex-start",
    shadowColor: "#000",
    shadowOpacity: 0.1,
    shadowOffset: { width: 0, height: 2 },
  },
  title: {
    fontSize: 18,
    fontWeight: "bold",
    marginBottom: 10,
  },
  activitiesCarousel: {
    width: "100%",
    height: 250, 
  },
  activitiesCard: {
    width: width, 
    padding: 10,
  },
  itemText: {
    marginVertical: 5,
  },
  dotsContainer: {
    flexDirection: "row",
    justifyContent: "center",
    marginTop: 10,
  },
  dot: {
    width: 10,
    height: 10,
    borderRadius: 5,
    backgroundColor: "#ccc",
    margin: 5,
  },
  activeDot: {
    backgroundColor: "#08c8f8",
  },
  button: {
    marginTop: 20,
  },
  buttonText: {
    fontSize: 16,
    fontWeight: "bold",
    color: "#fff",
  },
});

export default ProgramaBeneficiosClinicaScreen;
