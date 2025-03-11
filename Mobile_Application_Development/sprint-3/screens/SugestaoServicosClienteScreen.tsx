import React, { useState, useEffect } from "react";
import { View, Text, TouchableOpacity, StyleSheet, Alert, ScrollView } from "react-native";
import { db, auth } from "../src/firebaseConfig";
import { collection, getDocs, addDoc, query, where, doc, getDoc } from "firebase/firestore";
import Footer from "../components/Footer";

// Definindo a interface para a sugestão
interface Sugestao {
  id: string;
  idCliente: string;
  clinicaId: string;
  data: string;
  turno: string;
  status: string;
  idade: string;
  nome: string;
  // Adicione outros campos conforme necessário
}

const SugestaoServicosClienteScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const [sugestoes, setSugestoes] = useState<any[]>([]);
  const [userId, setUserId] = useState<string | null>(null);

  useEffect(() => {
    const fetchUserId = () => {
      const user = auth.currentUser;
      if (user) setUserId(user.uid);
    };
    fetchUserId();
  }, []);

  const fetchClinicaData = async (clinicaId: string) => {
    try {
      const clinicaDoc = await getDoc(doc(db, "t_dados_cadastrais_clinicas", clinicaId));
      if (clinicaDoc.exists()) {
        return clinicaDoc.data();
      } else {
        console.log("Clínica não encontrada");
        return null;
      }
    } catch (error) {
      console.error("Erro ao buscar dados da clínica:", error);
      return null;
    }
  };

  const fetchEspecialidadesClinica = async (clinicaId: string) => {
    try {
      const especialidadesDoc = await getDoc(doc(db, "t_especialidades", clinicaId));
      if (especialidadesDoc.exists()) {
        return especialidadesDoc.data().especialidades || [];
      } else {
        console.log("Especialidades não encontradas");
        return [];
      }
    } catch (error) {
      console.error("Erro ao buscar especialidades:", error);
      return [];
    }
  };

  const fetchEnderecoClinica = async (clinicaId: string) => {
    try {
      const enderecoDoc = await getDoc(doc(db, "t_endereco_clinica", clinicaId));
      if (enderecoDoc.exists()) {
        return enderecoDoc.data();
      } else {
        console.log("Endereço não encontrado");
        return null;
      }
    } catch (error) {
      console.error("Erro ao buscar endereço da clínica:", error);
      return null;
    }
  };

  useEffect(() => {
    const fetchSugestoes = async () => {
      if (!userId) return;
      try {
        const q = query(collection(db, "t_sugestao_consulta_clinica"), where("status", "==", "aceito"));
        const querySnapshot = await getDocs(q);

        if (querySnapshot.empty) {
          console.log("Nenhuma sugestão encontrada.");
        } else {
          const sugestoesData = await Promise.all(
            querySnapshot.docs.map(async (doc) => {
              const sugestao = { id: doc.id, ...doc.data() } as Sugestao;

              // Buscar dados da clínica
              const clinicaData = await fetchClinicaData(sugestao.clinicaId);
              const especialidadesData = await fetchEspecialidadesClinica(sugestao.clinicaId);
              const enderecoData = await fetchEnderecoClinica(sugestao.clinicaId);

              return {
                ...sugestao,
                clinicaData,
                especialidadesData,
                enderecoData,
              };
            })
          );
          setSugestoes(sugestoesData);
        }
      } catch (error) {
        console.error("Erro ao buscar sugestões:", error);
      }
    };
    fetchSugestoes();
  }, [userId]);

  const aceitarSugestao = async (sugestao: any) => {
    if (!userId) return;
    try {
      await addDoc(collection(db, "t_sugestao_consulta_cliente"), {
        id_cliente: userId,
        ...sugestao,
        status: "aceito"
      });
      setSugestoes((prev) => prev.filter((item) => item.id !== sugestao.id));
      Alert.alert("Sucesso", "Atendimento aceito com sucesso!");
    } catch (error) {
      console.error("Erro ao aceitar atendimento:", error);
      Alert.alert("Erro", "Não foi possível aceitar a sugestão.");
    }
  };

  const recusarSugestao = async (sugestao: any, motivo: string) => {
    if (!userId) return;
    try {
      await addDoc(collection(db, "t_motivo_recusa_cliente"), {
        idCliente: userId,
        idSugestao: sugestao.id,
        motivo,
        ...sugestao,
        status: "recusado"
      });
      setSugestoes((prev) => prev.filter((item) => item.id !== sugestao.id));
      Alert.alert("Sucesso", "Sugestão recusada com sucesso!");
    } catch (error) {
      console.error("Erro ao recusar atendimento:", error);
      Alert.alert("Erro", "Não foi possível recusar a sugestão.");
    }
  };

  return (
    <ScrollView contentContainerStyle={styles.scrollContainer} keyboardShouldPersistTaps="handled">
      <View style={styles.container}>
        <Text style={styles.title}>Sugestões de Atendimento</Text>
        {sugestoes.map((sugestao) => (
          <View key={sugestao.id} style={styles.card}>
            {/* Dados da Clínica */}
            {sugestao.clinicaData && (
              <View>
                <Text style={styles.TitleCard}>Clínica</Text>
                <Text style={styles.cardText}>Nome da Clínica: {sugestao.clinicaData.nome}</Text>
                <Text style={styles.cardText}>CNPJ: {sugestao.clinicaData.cnpj}</Text>
              </View>
            )}

            {/* Especialidades */}
            {sugestao.especialidadesData.length > 0 && (
              <View>
                {/*<Text style={styles.cardText}>Especialidades:</Text>*/}
                <Text style={styles.TitleCard}>Especialidades</Text>
                {sugestao.especialidadesData.map((especialidade: string, index: number) => (
                  <Text key={index} style={[styles.cardText, { marginLeft: 10 }]}>
                    • {especialidade}
                  </Text>
                ))}
              </View>
            )}

            {/* Endereço da Clínica */}
            {sugestao.enderecoData && (
              <View>
                 <Text style={styles.TitleCard}>Endereço</Text>
                <Text style={styles.cardText}>Bairro: {sugestao.enderecoData.bairro}</Text>
                <Text style={styles.cardText}>CEP: {sugestao.enderecoData.cep}</Text>
                <Text style={styles.cardText}>Cidade: {sugestao.enderecoData.cidade}</Text>
                <Text style={styles.cardText}>Estado: {sugestao.enderecoData.estado}</Text>
                <Text style={styles.cardText}>Rua: {sugestao.enderecoData.rua}</Text>
                <Text style={styles.cardText}>Número: {sugestao.enderecoData.numero}</Text>
              </View>
            )}

            {/* Dados da Consulta */}
            <View>
              <Text style={styles.TitleCard}>Importante</Text>
              <Text style={styles.cardText}>Data da Consulta: {sugestao.data}</Text>
              <Text style={styles.cardText}>Turno: {sugestao.turno}</Text>
            </View>

            <View style={styles.buttonContainer}>
              <TouchableOpacity style={[styles.button, { backgroundColor: "#024059" }]} onPress={() => aceitarSugestao(sugestao)}>
                <Text style={styles.buttonText}>Aceitar</Text>
              </TouchableOpacity>
              <TouchableOpacity style={[styles.button, { backgroundColor: "#ff5d4b" }]} onPress={() => recusarSugestao(sugestao, "Motivo informado pelo cliente")}>  
                <Text style={styles.buttonText}>Recusar</Text>
              </TouchableOpacity>
            </View>
          </View>
        ))}
      </View>
      <Footer textColor="#000" />
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: { 
    flex: 1, 
    padding: 20, 
    backgroundColor: "#f9f9f9", 
    width: '100%'
  },
  TitleCard: {
    fontSize: 18, 
    fontWeight: "bold", 
    marginBottom: 10, 
    color: "#024059"
  },
  scrollContainer: { flexGrow: 1, justifyContent: "center", alignItems: "center" },
  title: { fontSize: 32, fontWeight: "bold", marginBottom: 20, color: "#024059", },
  card: { backgroundColor: "#fff", padding: 15, borderRadius: 10, marginBottom: 15, shadowColor: "#000", shadowOpacity: 0.8 },
  cardText: { fontSize: 16, marginBottom: 5 },
  buttonContainer: { flexDirection: "row", justifyContent: "space-between", marginTop: 10 },
  button: { paddingVertical: 10, borderRadius: 8, width: "48%", alignItems: "center" },
  buttonText: { fontSize: 16, color: "#fff", fontWeight: "bold" }
});

export default SugestaoServicosClienteScreen;
