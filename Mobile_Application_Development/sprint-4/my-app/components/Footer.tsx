import React from "react";
import { View, Text, StyleSheet } from "react-native";

type TextFooter = {
  textColor: string;
}

const Footer: React.FC<TextFooter> = (
  { textColor = "#fff", }
) => {
  return (
    <View style={styles.footer}>
      <Text style={[styles.footerText, {color: textColor}]}>Delfos Machine</Text>
      <View style={styles.footerLine} />
    </View>
  );
};

const styles = StyleSheet.create({
  footer: {
    position: "absolute",
    bottom: 20,
    alignItems: "center",
  },
  footerText: {
    fontSize: 18,
    fontWeight: "bold",
    color: "#f8f8f8",
  },
  footerLine: {
    width: 100,
    height: 2,
    backgroundColor: "#ccc",
    marginTop: 5,
  },
});

export default Footer;
