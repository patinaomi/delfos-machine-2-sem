import React, { useState, useEffect } from "react";
import { View, Text, TextInput, Button, Alert, StyleSheet, TouchableOpacity, ScrollView, FlatList } from "react-native";
import { doc, getDoc, updateDoc } from "firebase/firestore";
import { db, auth } from "../src/firebaseConfig";
import CustomButton from "../components/CustomButton";
import Footer from "../components/Footer";
import { Picker } from "@react-native-picker/picker";

const ConsultarDadosMedicosScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const user = auth.currentUser;
  if (!user) {
    Alert.alert("Erro", "Nenhum usuário autenticado!");
    navigation.goBack();
    return null;
  }

  const [step, setStep] = useState(1);
  const [dados, setDados] = useState<any>({});
  const [mensagem, setMensagem] = useState("");
  const [loading, setLoading] = useState(true);
  const [especialidades, setEspecialidades] = useState<string[]>([]);
  const [especialidadeSelecionada, setEspecialidadeSelecionada] = useState<string | null>(null);

  // Lista de especialidades disponíveis neste momento
  const listaEspecialidades = [
    "Ortodontia",
    "Implantes Dentários",
    "Clínica Geral",
    "Prótese Dentária",
    "Odontologia Estética",
    "Cirurgia Bucal",
  ];

  const handleSelectEspecialidade = (especialidade: string) => {
    setEspecialidades(prevState => {
      if (prevState.includes(especialidade)) {
        return prevState.filter(item => item !== especialidade);
      } else {
        return [...prevState, especialidade];
      }
    });
  };


  useEffect(() => {
    const buscarDados = async () => {
      try {
        const dadosCadastraisRef = doc(db, "t_dados_cadastrais_medicos", user.uid);
        const dadosCadastraisSnap = await getDoc(dadosCadastraisRef);
  
        setDados((prevDados: any) => ({
          ...prevDados, // Mantém os dados existentes
          ...(dadosCadastraisSnap.exists() ? dadosCadastraisSnap.data() : {}),
        }));

        // Carregar especialidades
        if (dadosCadastraisSnap.exists()) {
          setEspecialidades(dadosCadastraisSnap.data().especialidades || []);
        }

        const dadosDisponiveisRef = doc(db, "t_dados_cadastrais_medicos", "todos");
        const dadosDisponiveisSnap = await getDoc(dadosDisponiveisRef);
        
  
      } catch (error) {
        console.error("Erro ao buscar dados:", error);
        Alert.alert("Erro", "Não foi possível buscar os dados.");
      } finally {
        setLoading(false);
      }
    };
  
    buscarDados();
  }, []);

  
  const atualizarDados = async (colecao: string, novosDados: object) => {
    try {
      const docRef = doc(db, colecao, user.uid);
      await updateDoc(docRef, {...novosDados, especialidade: especialidadeSelecionada});
      setMensagem("✅ Dados atualizados com sucesso!");
    } catch (error) {
      console.error("Erro ao atualizar dados:", error);
      setMensagem("❌ Erro ao atualizar os dados.");
    }
  };


  return (
    <ScrollView contentContainerStyle={styles.container}>
      {/*<Text style={styles.title}>Meus Dados</Text>*/}

      {loading ? (
        <Text>Carregando...</Text>
      ) : (
        <>
          {step === 1 && (
            <View style={styles.section}>
              <Text style={styles.sectionTitle}>Dados cadastrais</Text>
              <TextInput style={styles.input} placeholder="Nome" value={dados.nome || ""} onChangeText={(text) => setDados({ ...dados, nome: text })} />
              <TextInput style={styles.input} placeholder="Sobrenome" value={dados.sobrenome || ""}  />
              <TextInput style={styles.input} placeholder="CPF" value={dados.cpf || ""} />
              <TextInput style={styles.input} placeholder="Data Nascimento" value={dados.dataNascimento || ""} />
              
              <TextInput style={styles.input} placeholder="Especialidade" value={dados.especialidade || ""} editable={false}/>

              <Text style={styles.label}>Selecione uma Especialidade:</Text>
              <View style={styles.pickerContainer}>
                <Picker
                  selectedValue={especialidadeSelecionada}
                  onValueChange={(itemValue) => setEspecialidadeSelecionada(itemValue)}
                  style={styles.picker}
                  dropdownIconColor="#08c8f8"
                >
                  <Picker.Item label="Selecione uma opção..." value={null} />
                  {listaEspecialidades.map((especialidade) => (
                    <Picker.Item key={especialidade} label={especialidade} value={especialidade} />
                  ))}
                </Picker>
              </View>
              
              <CustomButton title="Atualizar" textColor="#fff" onPress={() => atualizarDados("t_dados_cadastrais_medicos", dados)} width={'100%'}/>
              

            </View>
          )}
          {mensagem ? <Text style={styles.mensagem}>{mensagem}</Text> : null}
        </>
      )}

      <Footer textColor='#000'/>
    </ScrollView>
  );
};

const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    justifyContent: "center",
    alignItems: "center",
    padding: 20,
    backgroundColor: "#f9f9f9",
    width:'100%',
    height: '100%',
  },
  title: {
    fontSize: 24,
    fontWeight: "bold",
    textAlign: "center",
    marginBottom: 20,
    marginTop:50,
  },
  section: {
    width: "100%",
    height:'100%',
    backgroundColor: "#fff",
    padding: 15,
    borderRadius: 10,
    marginBottom: 10,
    alignItems: "center",
  },
  sectionTitle: {
    fontSize: 18,
    fontWeight: "bold",
    marginBottom: 10,
  },
  input: {
    width: "100%",
    height: 40,
    borderWidth: 1,
    borderColor: "#ccc",
    borderRadius: 5,
    paddingLeft: 10,
    marginBottom: 10,
  },
  mensagem: {
    marginTop: 10,
    fontSize: 16,
    fontWeight: "bold",
    textAlign: "center",
  },
  prevButton: {
    marginTop: 10,
    padding: 10,
    backgroundColor: "#D32F2F",
    borderRadius: 10,
    width: "100%",
    alignItems: "center",
  },
  nextButton: {
    marginTop: 10,
    padding: 10,
    backgroundColor: "#2196F3",
    borderRadius: 10,
    width: "100%",
    alignItems: "center",
  },
  button: {
    minWidth: "100%",
    backgroundColor: "#08c8f8",
    paddingVertical: 14,
    borderRadius: 8,
    alignItems: "center",
    justifyContent: "center",
    marginTop: 10,
  },
  buttonText: {
    fontSize: 16,
    fontWeight: "bold",
    color: "#fff",
  },
  itemContainer: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: 5,
    width: "100%",
  },
  itemText: {
    fontSize: 16,
    color: "#333",
  },
  sectionSubTitle: {
    fontSize: 16,
    marginBottom: 10,
    fontWeight: 'bold',
  },
  specialityItem: {
    padding: 10,
    borderWidth: 1,
    borderColor: "#08c8f8",
    borderRadius: 8,
    marginBottom: 10,
    width:'100%',
  },
  selectedSpeciality: {
    backgroundColor: "#08c8f8",
    color:'white',
  },
  specialityText: {
    fontSize: 16,
    color: "#081828",
    textAlign: "center",
  },
  label: {
    fontSize: 16,
    marginBottom: 10,
    color: "#000",
    textAlign: 'left',
    width: '100%',
  },
  pickerContainer: {
    borderWidth: 1,
    borderColor: "#08c8f8",
    borderRadius: 8,
    width: '100%',
    marginBottom:20,
  },
  picker: {
    color: "#000",
  },
  
});


export default ConsultarDadosMedicosScreen;
