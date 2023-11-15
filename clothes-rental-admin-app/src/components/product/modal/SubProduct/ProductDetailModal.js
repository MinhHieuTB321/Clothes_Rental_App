import { useFormik } from "formik";
import { useState,useEffect } from "react";
import Alert from "../../../common/Alert";
import { Actions, useAPIRequest } from "../../../common/api-request";
import { DefaultButton, PrimaryButton } from "../../../common/Buttons";
import { Input } from "../../../common/FormControls";
import { parseError } from "../../../common/utils";
import SubProductImages, { Images } from "./SubProductImages";
import { saveProduct,addSubProduct } from "../../ProductRepo";

export function SubProductEdit({subProduct = { name: "" }, handleClose }) {
    const [state, requestSave] = useAPIRequest(saveProduct);
  
  
    const formik = useFormik({
      enableReinitialize: true,
      initialValues: { ...subProduct },
      validate: (values) => {
        let errors = {};
        if (!values.size) {
          errors.size = "Please enter size.";
        }
  
        if (!values.color || values.color.trim().length === 0) {
          errors.color = "Please enter color.";
        }
        if (!values.material || values.material.trim().length === 0) {
          errors.material = "Please enter material.";
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
            label="Color:"
            name="color"
            placeholder="Enter color . . ."
            value={formik.values.color}
            onChange={formik.handleChange}
            error={formik.errors.color}
          />
  
          <Input
            label="Size:"
            name="size"
            placeholder="Enter size  . . ."
            value={formik.values.size}
            onChange={formik.handleChange}
            error={formik.errors.size}
          />
  
          <Input
            label="Material:"
            name="material"
            placeholder="Enter material . . ."
            value={formik.values.material}
            onChange={formik.handleChange}
            error={formik.errors.material}
          />
        </div>
  
        <hr className="lg:col-span-2 my-2" />
  
        <div className="lg:col-span-2">
        <label className="form-control-label mb-2">
          Images *
        </label>
          <SubProductImages
            id={subProduct.id}
            //images={formik.values.images}
            images={subProduct.productImages}
            onImagesChange={(blobs, urls) => {
              formik.setFieldValue("files", blobs);
              formik.setFieldValue("urls", urls);
            }}
          />
        </div>
        <div className="flex flex-row-reverse space-x-reverse space-x-2">
          <PrimaryButton
            type="submit"
            disabled={formik.isSubmitting}
            loading={formik.isSubmitting}
          >
            Save
          </PrimaryButton>
          <DefaultButton disabled={formik.isSubmitting} onClick={handleClose}>
            Cancel
          </DefaultButton>
        </div>
      </form>
    );
}


export function SubProductAdd({subProduct = { name: "" }, handleClose }) {
  console.log(subProduct);
  const [state, requestSave] = useAPIRequest(addSubProduct);

  const formik = useFormik({
    enableReinitialize: true,
    initialValues: { ...subProduct },
    validate: (values) => {
      let errors = {};
      if (!values.size) {
        errors.size = "Please enter size.";
      }

      if (!values.color || values.color.trim().length === 0) {
        errors.color = "Please enter color.";
      }
      if (!values.material || values.material.trim().length === 0) {
        errors.material = "Please enter material.";
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
          label="Color:"
          name="color"
          placeholder="Enter color . . ."
          value={formik.values.color}
          onChange={formik.handleChange}
          error={formik.errors.color}
        />

        <Input
          label="Size:"
          name="size"
          placeholder="Enter size  . . ."
          value={formik.values.size}
          onChange={formik.handleChange}
          error={formik.errors.size}
        />

        <Input
          label="Material:"
          name="material"
          placeholder="Enter material . . ."
          value={formik.values.material}
          onChange={formik.handleChange}
          error={formik.errors.material}
        />
      </div>

      <hr className="lg:col-span-2 my-2" />

      <div className="lg:col-span-2">
      <label className="form-control-label mb-2">
        Images *
      </label>
        <Images
          //images={formik.values.images}
          onImagesChange={(blobs, urls) => {
            formik.setFieldValue("files", blobs);
            formik.setFieldValue("urls", urls);
          }}
        />
      </div>
      <div className="flex flex-row-reverse space-x-reverse space-x-2">
        <PrimaryButton
          type="submit"
          disabled={formik.isSubmitting}
          loading={formik.isSubmitting}
        >
          Save
        </PrimaryButton>
        <DefaultButton disabled={formik.isSubmitting} onClick={handleClose}>
          Cancel
        </DefaultButton>
      </div>
    </form>
  );
}