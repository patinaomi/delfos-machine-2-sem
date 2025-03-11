import React, { useState } from "react";
import { View, Text, TextInput, Button, Alert, StyleSheet, TouchableOpacity } from "react-native";
import { doc, setDoc } from "firebase/firestore";
import { db, auth } from "../src/firebaseConfig";
import { Picker } from "@react-native-picker/picker";

const CadastrarMedicosScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const user = auth.currentUser;
  if (!user) {
    Alert.alert("Erro", "Nenhum usuário autenticado!");
    navigation.goBack();
    return null;
  }

  const [step, setStep] = useState(1);
  
  const [nome, setNome] = useState("");
  const [sobrenome, setSobrenome] = useState("");
  const [cpf, setCpf] = useState("");
  const [dataNascimento, setDataNascimento] = useState("");
  const [email, setEmail] = useState("");
  const [especialidade, setEspecialidade] = useState("");
  const [especialidadeSelecionada, setEspecialidadeSelecionada] = useState<string>(""); 

  const listaEspecialidades = [
    "Ortodontia",
    "Implantes Dentários",
    "Clínica Geral",
    "Prótese Dentária",
    "Odontologia Estética",
    "Cirurgia Bucal",
  ];
  
  const [mensagem, setMensagem] = useState("");

  const salvarDados = async (colecao: string, dados: object) => {

    if (!especialidadeSelecionada) {
      setMensagem("❌ Por favor, selecione uma especialidade.");
      return;  // Bloqueia caso não selecione uma opção.
    }

    try {
      await setDoc(doc(db, colecao, user.uid), dados, { merge: true });
      setMensagem("✅ Dados salvos com sucesso!");
    } catch (error: any) {
      setMensagem("❌ Erro ao salvar os dados.");
    }
  };

  const handleConsultarDadosMedicos = async () => {
    navigation.navigate("ConsultarDadosMedicos");
  }

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Delfos Machine</Text>
      <View style={styles.tittleLine} />

      {step === 1 && (
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Especialistas</Text>
          <TextInput style={styles.input} placeholder="Nome" value={nome} onChangeText={setNome} />
          <TextInput style={styles.input} placeholder="Sobrenome" value={sobrenome} onChangeText={setSobrenome} />
          <TextInput style={styles.input} placeholder="CPF" value={cpf} onChangeText={setCpf} />
          <TextInput style={styles.input} placeholder="Data de Nascimento" value={dataNascimento} onChangeText={setDataNascimento} />
          <TextInput style={styles.input} placeholder="Email" value={email} keyboardType="email-address" onChangeText={setEmail} />
          
          {/*<TextInput style={styles.input} placeholder="Especialidade" value={especialidade} onChangeText={setEspecialidade} />*/}

          <Text style={styles.label}>Selecione uma Especialidade:</Text>
          <View style={styles.pickerContainer}>
            <Picker
              selectedValue={especialidadeSelecionada}
              onValueChange={(itemValue) => setEspecialidadeSelecionada(itemValue)}
              style={styles.picker}
              dropdownIconColor="#08c8f8"
            >
              <Picker.Item label="Selecione uma opção..." value="" />
              {listaEspecialidades.map((especialidade) => (
                <Picker.Item key={especialidade} label={especialidade} value={especialidade} />
              ))}
            </Picker>
          </View>
          
          <TouchableOpacity style={styles.buttonSave} onPress={() => salvarDados("t_dados_cadastrais_medicos", { nome, sobrenome, cpf, dataNascimento, especialidade:especialidadeSelecionada })}>
            <Text style={styles.buttonText}>Salvar</Text>
          </TouchableOpacity>

          <TouchableOpacity style={styles.buttonNavigate} onPress={handleConsultarDadosMedicos}>
            <Text style={styles.buttonText}>Consultar lista de médicos</Text>
          </TouchableOpacity>

          {mensagem ? <Text style={styles.mensagem}>{mensagem}</Text> : null}
          
        </View>
      )}

    </View>
  );
};

const styles = StyleSheet.create({
  container: { 
    flex: 1, 
    padding: 20, 
    backgroundColor: "#f9f9f9", 
    justifyContent: "center",
    width: '100%'
  },
  title: { 
    fontSize: 24, 
    fontWeight: "bold", 
    textAlign: "center", 
    marginBottom: 20 ,
    color: "#0A4275"
  },
  tittleLine: {
    width: 100,
    height: 2,
    backgroundColor: "#ccc",
    marginTop: 5,
  },
  section: { 
    backgroundColor: "#fff", 
    padding: 15, 
    borderRadius: 10, 
    alignItems: "center",
    width: '100%',
  },
  sectionTitle: { 
    fontSize: 18, 
    fontWeight: "bold", 
    marginBottom: 10,
    color:"#0A4275",
  },
  input: { width: "100%", height: 40, borderBottomWidth: 1, borderColor: "#ccc", marginBottom: 10 },
  mensagem: { marginTop: 10, fontSize: 14, color: "green" },
  nextButton: { 
    marginTop: 10, 
    padding: 10, 
    backgroundColor: "#2196F3", 
    borderRadius: 10,
    width: '90%',
    alignItems: 'center'
  },
  prevButton: { 
    marginTop: 10, 
    padding: 10, 
    backgroundColor: "#D32F2F", 
    borderRadius: 10,
    width: '90%',
    alignItems: 'center'
  },
  buttonText: { 
    fontSize: 16, 
    color: "#fff", 
    fontWeight: "bold" },
  buttonSave: {
    width: "100%",
    backgroundColor: "#08c8f8",
    paddingVertical: 14,
    borderRadius: 8,
    alignItems: "center",
    justifyContent: "center",
    marginTop: 10,
    fontWeight: "bold",
    color: '#fff'
  },
  buttonNavigate: {
    width: "100%",
    backgroundColor: "#0A4275",
    paddingVertical: 14,
    borderRadius: 8,
    alignItems: "center",
    justifyContent: "center",
    marginTop: 10,
    fontWeight: "bold",
    color: '#fff'
  },
  label: {
    fontSize: 16,
    marginBottom: 10,
    color: "#000",
    width:'100%',
    textAlign: 'left',
  },
  pickerContainer: {
    borderWidth: 1,
    borderColor: "#08c8f8",
    borderRadius: 8,
    overflow: "hidden",
    backgroundColor: "#fff",
    width: "100%",
  },
  picker: {
    color: "#000",
  },
});

export default CadastrarMedicosScreen;
