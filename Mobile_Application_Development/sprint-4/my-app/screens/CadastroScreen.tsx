import React, { useState } from 'react';
import { View, Text, TextInput, Button, Alert, StyleSheet, ImageBackground, TouchableOpacity } from 'react-native';
import { collection, addDoc, setDoc, doc } from "firebase/firestore";
import { db, auth } from "../src/firebaseConfig"; // Importando a configuração do Firebase
import { createUserWithEmailAndPassword } from 'firebase/auth';
import CustomButton from '../components/CustomButton';
import Footer from '../components/Footer';

const CadastroScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [perfil, setPerfil] = useState('');
  const [isFocused, setIsFocused] = useState(false);

  const handleCadastro = async () => {
    console.log("🔹 Botão pressionado! Tentando cadastrar usuário...");
    try {

      const userCredential = await createUserWithEmailAndPassword(auth, email, senha);
      const user = userCredential.user;

      // Definir o perfil automaticamente como 'comum' para ajudar no login entre as páginas.
      const perfil = "comum"; 

      console.log("✅ Usuário cadastrado com sucesso! ID:", user.uid);
      Alert.alert("Sucesso", "Usuário cadastrado com sucesso!");

      await setDoc(doc(collection(db, "t_usuarios"), user.uid), {
        email: email,
        perfil: perfil,
        criadoEm: new Date().toISOString(),

      });
      
      console.log("✅ Dados do usuário salvos no Firestore!");
      Alert.alert("Sucesso", "Usuário cadastrado com sucesso!");

      navigation.navigate("Sucesso");

    } catch (error) {
      console.error("❌ Erro ao cadastrar usuário:", error);
      Alert.alert("Erro", "Não foi possível cadastrar o usuário.");
    }
  };

  return (
    <ImageBackground source={require("../assets/background/tela-cadastro-dois.png")} style={styles.background}>

      <View style={styles.formContainer}>
        <Text style={styles.title}>Cadastro</Text>
        <TextInput
          style={[styles.input, isFocused && styles.inputFocused]}
          placeholder="Email"
          keyboardType="email-address"
          autoCapitalize="none"
          value={email}
          onChangeText={setEmail}
        />
        <TextInput
          style={styles.input}
          placeholder="Senha"
          secureTextEntry
          value={senha}
          onChangeText={setSenha}
        />

        <CustomButton title="Cadastrar" textColor='#fff' onPress={handleCadastro} width={'100%'}/>
        
      </View>

      <Footer textColor='#fff'/>

    </ImageBackground>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 10,
    width: '100%',
  },
  formContainer: {
    width: "90%",
    backgroundColor: "rgba(255, 255, 255, 0.7)",
    padding: 20,
    borderRadius: 12,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.2,
    shadowRadius: 4,
    elevation: 5,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
    textAlign: 'center',
  },
  input: {
    minWidth: '90%',
    height: 40,
    borderWidth: 1,
    borderColor: "#08c8f8",
    backgroundColor: '#fff',
    borderRadius: 5,
    paddingLeft: 5,
    marginBottom: 10,
  },
  inputFocused: {
    backgroundColor: "white", 
  },
  background: {
    flex: 1,
    resizeMode: "cover",
    justifyContent: "center",
    alignItems: "center",
    width: "100%",
    height: "100%",
    opacity: 20,
  },
  overlay: {
    flex: 1,
    backgroundColor: "rgba(0, 0, 0, 0.3)",
    justifyContent: "center",
    alignItems: "center",
    width: "100%"
  },
  
});

export default CadastroScreen;
