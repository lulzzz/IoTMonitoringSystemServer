export const isUserAuthenticated = () => {
  const user = localStorage.getItem("CURRENT_USER");
  if (user !== null) {
    return true;
  } else return false;
};

export const getLoggedInUser = () => {
  var user = {
    access_token: "",
    email: "",
    role: "",
    userName: ""
  };
  if (isUserAuthenticated()) {
    var userData = JSON.parse(localStorage.getItem("CURRENT_USER"));
    user = {
      access_token: userData.access_token,
      email: userData.email,
      role: userData.role,
      userName: userData.userName
    };
  }
  return user;
};
