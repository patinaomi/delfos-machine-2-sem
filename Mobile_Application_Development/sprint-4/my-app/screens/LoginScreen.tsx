import React, { useState } from "react";
import { View, Text, TextInput, Button, Alert, StyleSheet, TouchableOpacity, ImageBackground } from "react-native";
import { signInWithEmailAndPassword } from "firebase/auth";
import { auth, db } from "../src/firebaseConfig"; 
import GoogleAuth from "../src/GoogleAuth";
import CustomButton from "../components/CustomButton";
import Footer from "../components/Footer";
import { doc, getDoc } from "firebase/firestore";

const LoginScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const [email, setEmail] = useState("");
  const [senha, setSenha] = useState("");

  const handleLogin = async () => {
    try {
      console.log("🔹 Tentando fazer login...");

      //await signInWithEmailAndPassword(auth, email, senha);
      const userCredential = await signInWithEmailAndPassword(auth, email, senha); 
      const user = userCredential.user;
      
      {/*console.log("✅ Login bem-sucedido!");
      Alert.alert("Sucesso", "Login realizado com sucesso!");

    navigation.navigate("SessaoRestrita");*/}

    const userDocRef = doc(db, "t_usuarios", user.uid);
      const userDoc = await getDoc(userDocRef);

      if (userDoc.exists()) {
        const userData = userDoc.data();
        const perfil = userData?.perfil;

        if (perfil === "comum") {
          console.log("✅ Login bem-sucedido!");
          Alert.alert("Sucesso", "Login realizado com sucesso!");

          // Navega para a Sessão Restrita de clientes
          navigation.navigate("SessaoRestrita");
        } else {
          console.error("❌ Perfil inválido");
          Alert.alert("Erro", "Você não tem permissão para acessar esta área.");
        }
      } else {
        console.error("❌ Usuário não encontrado no Firestore");
        Alert.alert("Erro", "Usuário não encontrado.");
      }


    } catch (error: any) {
      console.error("❌ Erro ao fazer login:", error.message);
      Alert.alert("Erro", "Email ou senha incorretos.");
    }
  };



  return (
    <ImageBackground source={require("../assets/background/opcao-um.png")} style={styles.background}>

      <View style={styles.formContainer}>
        <Text style={styles.title}>Login</Text>
        <TextInput
          style={styles.input}
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

        <CustomButton title="Login" textColor="#fff" onPress={handleLogin} width={'100%'}/>

        {/* Botão de Login com Google, para facilitar o acesso. */}
        {/*<GoogleAuth navigation={navigation}/>*/}
        
      </View>

      <Footer textColor='#fff'/>

    </ImageBackground>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    padding: 20,
    width: '100%'
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
    fontWeight: "bold",
    marginBottom: 20,
    textAlign: "center",
  },
  input: {
    width: "100%",
    height: 40,
    borderWidth: 1,
    borderColor: "#08c8f8",
    borderRadius: 5,
    paddingLeft: 10,
    marginBottom: 10,
    backgroundColor: '#fff'
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

export default LoginScreen;
function login(token: string) {
  throw new Error("Function not implemented.");
}

