import React, { useState } from "react";
import { View, Text, TextInput, Alert, StyleSheet, ImageBackground, KeyboardAvoidingView, Platform, ScrollView, TouchableWithoutFeedback, Keyboard } from "react-native";
import { signInWithEmailAndPassword } from "firebase/auth";
import { auth, db } from "../src/firebaseConfig"; 
import CustomButton from "../components/CustomButton";
import Footer from "../components/Footer";
import { doc, getDoc } from "firebase/firestore";

const LoginClinicaScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const [email, setEmail] = useState("");
  const [senha, setSenha] = useState("");

  const handleLogin = async () => {
    try {
      console.log("🔹 Tentando fazer login...");

      const userCredential = await signInWithEmailAndPassword(auth, email, senha); 
      const user = userCredential.user;

      const userDocRef = doc(db, "t_clinicas", user.uid);
      const userDoc = await getDoc(userDocRef);

      if (userDoc.exists()) {
        const userData = userDoc.data();
        const perfil = userData?.perfil;

        if (perfil === "clinica") {
          console.log("✅ Login bem-sucedido!");
          Alert.alert("Sucesso", "Login realizado com sucesso!");

          // Navega para a Sessão Restrita da Clínica
          navigation.navigate("SessaoRestritaClinica");
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
    <ImageBackground source={require("../assets/background/login-clinica.png")} style={styles.background}>
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
              <Text style={styles.title}>Login</Text>
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

              <CustomButton title="Login" textColor='#fff' onPress={handleLogin} width={'100%'} />
            </View>

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
    paddingHorizontal: 10,
  },
  formContainer: {
    width: "100%",
    maxWidth: 400,
    backgroundColor: "rgba(255, 255, 255, 0.7)", 
    padding: 15,
    borderRadius: 12,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.5,
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
    minWidth: "80%",
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

export default LoginClinicaScreen;
