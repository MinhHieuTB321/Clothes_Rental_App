import { useFormik } from "formik";
import { useEffect } from "react";
import Alert from "../../common/Alert";
import { Actions, useAPIRequest } from "../../common/api-request";
import { DefaultButton, PrimaryButton } from "../../common/Buttons";
import { Input } from "../../common/FormControls";
import { parseError } from "../../common/utils";
import { addPrice, savePrice } from "../ComboRepo";

export function PriceEdit({ price = { name: "" }, handleClose }) {
  const [state, requestSave] = useAPIRequest(savePrice);
  const formik = useFormik({
    enableReinitialize: true,
    initialValues: { ...price },
    validate: (values) => {
      let errors = {};
      if (!values.duration || values.duration.trim().length === 0) {
        errors.duration = "Please enter duration.";
      }

      if (!values.deposit) {
        errors.deposit = "Please enter deposit.";
      }

      if (!values.rentalPrice) {
        errors.rentalPrice = "Please enter rental price.";
      }
      return errors;
    },
    validateOnBlur: false,
    validateOnChange: false,
    onSubmit: (values) => {
      requestSave(values);
    },
  });

  useEffect(() => {
    if (state.status !== Actions.loading) {
      formik.setSubmitting(false);
    }

    if (state.status === Actions.success) {
      handleClose(true);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [state]);

  return (
    <form onSubmit={formik.handleSubmit} className="flex flex-col mt-4">
      {state.status === Actions.failure && (
        <Alert alertClass="alert-error mb-4" closeable>
          {parseError(state.error)}
        </Alert>
      )}

      <div className="mb-6">
        <Input
          label="Duration *"
          name="duration"
          placeholder="Enter duration "
          value={formik.values.duration}
          onChange={formik.handleChange}
          error={formik.errors.duration}
        />

        <Input
          label="Deposit *"
          name="deposit"
          placeholder="Enter deposit"
          value={formik.values.deposit}
          onChange={formik.handleChange}
          error={formik.errors.deposit}
        />

        <Input
          label="Rental Price *"
          name="rentalPrice"
          placeholder="Enter rental price"
          value={formik.values.rentalPrice}
          onChange={formik.handleChange}
          error={formik.errors.rentalPrice}
        />
      </div>
      <div className="flex flex-row-reverse space-x-reverse space-x-2">
        <PrimaryButton type="submit" disabled={formik.isSubmitting} loading={formik.isSubmitting}>
          Save
        </PrimaryButton>
        <DefaultButton disabled={formik.isSubmitting} onClick={handleClose}>
          Cancel
        </DefaultButton>
      </div>
    </form>
  );
}

export function PriceAdd({ comboId, price = { name: "" }, handleClose }) {
  const [state, requestAdd] = useAPIRequest(addPrice);

  const formik = useFormik({
    enableReinitialize: true,
    initialValues: { ...price },
    validate: (values) => {
      let errors = {};
      if (!values.duration || values.duration.trim().length === 0) {
        errors.duration = "Please enter duration.";
      }

      if (!values.deposit || values.deposit.trim().length === 0) {
        errors.deposit = "Please enter deposit.";
      }

      if (!values.rentalPrice || values.rentalPrice.trim().length === 0) {
        errors.rentalPrice = "Please enter rental price.";
      }
      return errors;
    },
    validateOnBlur: false,
    validateOnChange: false,
    onSubmit: (values) => {
      requestAdd({comboId,values});
    },
  });

  useEffect(() => {
    if (state.status !== Actions.loading) {
      formik.setSubmitting(false);
    }

    if (state.status === Actions.success) {
      handleClose(true);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [state]);

  return (
    <form onSubmit={formik.handleSubmit} className="flex flex-col mt-4">
      {state.status === Actions.failure && (
        <Alert alertClass="alert-error mb-4" closeable>
          {parseError(state.error)}
        </Alert>
      )}

      <div className="mb-6">
      <Input
          label="Duration *"
          name="duration"
          placeholder="Enter duration "
          value={formik.values.duration}
          onChange={formik.handleChange}
          error={formik.errors.duration}
        />

        <Input
          label="Deposit *"
          name="deposit"
          placeholder="Enter deposit"
          value={formik.values.deposit}
          onChange={formik.handleChange}
          error={formik.errors.deposit}
        />

        <Input
          label="Rental Price *"
          name="rentalPrice"
          placeholder="Enter rental price"
          value={formik.values.rentalPrice}
          onChange={formik.handleChange}
          error={formik.errors.rentalPrice}
        />
      </div>
      <div className="flex flex-row-reverse space-x-reverse space-x-2">
        <PrimaryButton type="submit" disabled={formik.isSubmitting} loading={formik.isSubmitting}>
          Save
        </PrimaryButton>
        <DefaultButton disabled={formik.isSubmitting} onClick={handleClose}>
          Cancel
        </DefaultButton>
      </div>
    </form>
  );
}


