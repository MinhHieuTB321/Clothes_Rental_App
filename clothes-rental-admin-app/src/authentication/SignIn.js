import { useFormik } from "formik";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Alert from "../components/common/Alert";
import { Actions, useAPIRequest } from "../components/common/api-request";
import { PrimaryButton } from "../components/common/Buttons";
import Card from "../components/common/Card";
import { Input, PasswordInput } from "../components/common/FormControls";
import Footer from "../components/template/Footer";
import { parseAuthError } from "./AuthErrors";
import { signIn,getToken } from "./AuthRepo";

function SignIn() {
  const navigate = useNavigate();
  const [state, requestSignIn] = useAPIRequest(signIn);
  const [token,requestToken]=useAPIRequest(getToken);

  const formik = useFormik({
    initialValues: { email: "owner@gmail.com", password: "123456" },
    validate: (values) => {
      let errors = {};
      if (!values.email) {
        errors.email = "Please enter email address.";
      }

      if (!values.password) {
        errors.password = "Please enter password.";
      }
      return errors;
    },
    validateOnBlur: false,
    validateOnChange: false,
    onSubmit: (values) => {
      requestSignIn(values);
      requestToken(values);
    },
  });

  useEffect(() => {
    if (state.status !== Actions.loading) {
      formik.setSubmitting(false);
    }

    if (state.status === Actions.success) {
      localStorage.setItem("Token",token.payload.accessToken);
      localStorage.setItem("Role",token.payload.role);
      localStorage.setItem("Id",token.payload.id);
      navigate("/", { replace: true });
    }
  }, [state]);

  return (
    <div className="overflow-y-auto h-screen flex flex-col">
      <div className="flex-shrink-0 p-5">
        <div className="grid grid-cols-1 sm:grid-cols-4 lg:grid-cols-6 xl:grid-cols-8">
          <div className="sm:col-start-2 lg:col-start-3 xl:col-start-4 col-span-2 mt-1">
            <div className="flex flex-col">
              <img
                className="aspect-square w-[90px] mx-auto rounded-full mb-5"
                src="/logo.png"
                alt="Logo"
              />
              <Card>
                <Card.Body>
                  <form onSubmit={formik.handleSubmit}>
                    <div className="flex flex-col space-y-4">
                      <h3 className="mb-2">Sign In</h3>

                      {state.status === "failure" && (
                        <Alert alertClass="alert-error mb-4" closeable>
                          {parseAuthError(state.error.code)}
                        </Alert>
                      )}

                      <Input
                        className="mb-4"
                        label="Email"
                        name="email"
                        type="email"
                        placeholder="name@domain.com"
                        value={formik.values.email}
                        onChange={formik.handleChange}
                        error={formik.errors.email}
                      />

                      <PasswordInput
                        label="Password"
                        name="password"
                        value={formik.values.password}
                        onChange={formik.handleChange}
                        error={formik.errors.password}
                      />

                      <div className="pt-2 flex">
                        <PrimaryButton
                          type="submit"
                          className="w-full"
                          disabled={formik.isSubmitting}
                          loading={formik.isSubmitting}
                        >
                          Login
                        </PrimaryButton>
                      </div>
                    </div>
                  </form>
                </Card.Body>
              </Card>
            </div>
          </div>
        </div>
      </div>
      <div className="mt-auto"></div>

      <Footer />
    </div>
  );
}

export default SignIn;
