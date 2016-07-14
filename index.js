const crypto = require('crypto');

const Alg = 'aes-128-cbc';
const Key = '5TGB&YHN7UJM(IK<';
const IV  = '!QAZ2WSX#EDC4RFV';

function Encrypt(plainText) {
  const cipher = crypto.createCipheriv(Alg, Key, IV);
  var encrypted = cipher.update(plainText, 'utf8', 'base64');
  return encrypted += cipher.final('base64');
}

function Decrypt(encryptedText) {
  const decipher = crypto.createDecipheriv(Alg, Key, IV);
  var decrypted = decipher.update(encryptedText, 'base64', 'utf8');
  return decrypted += decipher.final('utf8');
}

// utcDate = new Date().toISOString();
var encrypted = Encrypt("1607312359AB01;2016-07-14T08:53:34.589Z");

console.log("authToken=" + encrypted);
console.log("decrypted=" + Decrypt(encrypted));
