import React, { useState } from 'react';
import { View, Text, TextInput, Alert, StyleSheet, ImageBackground, KeyboardAvoidingView, Platform, ScrollView, TouchableWithoutFeedback, Keyboard } from 'react-native';
import { collection, setDoc, doc } from "firebase/firestore";
import { db, auth } from "../src/firebaseConfig"; 
import { createUserWithEmailAndPassword } from 'firebase/auth';
import CustomButton from '../components/CustomButton';
import Footer from '../components/Footer';

const CadastroClinicaScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const [nome, setNome] = useState('');
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [perfil, setPerfil] = useState('');

  const handleCadastro = async () => {
    console.log("🔹 Botão pressionado! Tentando cadastrar clínica...");
    try {
      const userCredential = await createUserWithEmailAndPassword(auth, email, senha);
      const user = userCredential.user;

      console.log("✅ Clínica cadastrada com sucesso! ID:", user.uid);
      Alert.alert("Sucesso", "Clínica cadastrada com sucesso!");

      // Definir o perfil automaticamente como 'clinica' para ajudar no login entre as páginas.
      const perfil = "clinica"; 

      await setDoc(doc(collection(db, "t_clinicas"), user.uid), {
        email: email,
        perfil: perfil,
        criadoEm: new Date().toISOString(),
      });

      console.log("✅ Dados da clínica salvos no banco de dados!");
      Alert.alert("Sucesso", "Clínica cadastrada com sucesso!");

      navigation.navigate("Sucesso");

    } catch (error) {
      console.error("❌ Erro ao cadastrar clínica:", error);
      Alert.alert("Erro", "Não foi possível cadastrar a clínica.");
    }
  };

  return (
    <ImageBackground source={require("../assets/background/cadastro-clinica.png")} style={styles.background}>
      <KeyboardAvoidingView
        behavior={Platform.OS === "ios" ? "padding" : "height"}
        style={styles.container}
      >
        <TouchableWithoutFeedback onPress={Keyboard.dismiss}>
          <ScrollView 
            contentContainerStyle={styles.scrollContainer} 
            keyboardShouldPersistTaps="handled"
          >
  
            <View style={styles.formContainer}>
              <Text style={styles.title}>Cadastro</Text>
              <TextInput
                style={styles.input}
                placeholder="Nome da clínica"
                placeholderTextColor="#555"
                autoCapitalize="words"
                value={nome}
                onChangeText={setNome}
              />
              <TextInput
                style={styles.input}
                placeholder="Email"
                placeholderTextColor="#555"
                keyboardType="email-address"
                autoCapitalize="none"
                value={email}
                onChangeText={setEmail}
              />
              <TextInput
                style={styles.input}
                placeholder="Senha"
                placeholderTextColor="#555"
                secureTextEntry
                value={senha}
                onChangeText={setSenha}
              />

              <CustomButton title="Cadastrar" textColor='#fff' onPress={handleCadastro} width={'100%'} />
            </View>

            {/* Rodapé */}
            <Footer textColor='#fff' />
          </ScrollView>
        </TouchableWithoutFeedback>
      </KeyboardAvoidingView>
    </ImageBackground>
  );
};

const styles = StyleSheet.create({
  background: {
    flex: 1,
    resizeMode: "cover",
    justifyContent: "center",
    alignItems: "center",
    width: "100%",
    height: "100%",
  },
  container: {
    flex: 1,
  },
  scrollContainer: {
    flexGrow: 1,
    justifyContent: "center",
    alignItems: "center",
    paddingHorizontal: 20,
  },
  formContainer: {
    width: "100%",
    backgroundColor: "rgba(255, 255, 255, 0.9)",
    padding: 20,
    borderRadius: 12,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 4,
    elevation: 5,
  },
  title: {
    fontSize: 22,
    fontWeight: "bold",
    color: "#024059",
    textAlign: "center",
    marginBottom: 20,
  },
  input: {
    minWidth: "100%",
    height: 50,
    backgroundColor: "#fff",
    borderWidth: 1,
    borderColor: "#024059",
    borderRadius: 8,
    paddingLeft: 10,
    marginBottom: 15,
    fontSize: 16,
  },
});

export default CadastroClinicaScreen;
