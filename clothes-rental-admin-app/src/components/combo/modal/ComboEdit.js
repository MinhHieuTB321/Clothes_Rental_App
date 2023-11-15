import { useFormik } from "formik";
import { useState,useEffect,useContext } from "react";
import { LoadingContext } from "../../common/Contexts";
import Alert from "../../common/Alert";
import { Actions, useAPIRequest } from "../../common/api-request";
import { DefaultButton, PrimaryButton,DangerButton } from "../../common/Buttons";
import { Input } from "../../common/FormControls";
import { parseError } from "../../common/utils";
import { addCombo,saveCombo,getPrices,deletePrice } from "../ComboRepo";
import ComboImages from "../ComboImages";
import { toast } from "react-toastify";
import { PencilAltIcon ,TrashIcon} from "@heroicons/react/outline";
import Modal,{ConfirmModal} from "../../common/Modal";
import Table from "../../common/Table";
import Card from "../../common/Card";
import { Role } from "../../../constants";
import { PlusIcon } from "@heroicons/react/solid";
import { PriceEdit,PriceAdd } from "../price/PriceEdit";



export function ComboEdit({combo = { name: "" }, handleClose }) {
  const [state, requestSave] = useAPIRequest(saveCombo);
  const [listImages, setListImages] = useState([]);

  useEffect(() => {
    setListImages(prevList => [...prevList, combo.fileUrl]);
  }, [combo.fileUrl]);

  const formik = useFormik({
    enableReinitialize: true,
    initialValues: { ...combo },
    validate: (values) => {
      let errors = {};
      if (!values.comboName || values.comboName.trim().length === 0) {
        errors.comboName = "Please enter combo name.";
      }

      if (!values.description || values.description.trim().length === 0) {
        errors.description = "Please enter description.";
      }
      if (!values.quantity || values.quantity.trim().length === 0) {
        errors.quantity = "Please enter quantity.";
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
          label="Combo Name:"
          name="comboName"
          placeholder="Enter name . . ."
          value={formik.values.comboName}
          onChange={formik.handleChange}
          error={formik.errors.comboName}
        />

        <Input
          label="Description:"
          name="description"
          placeholder="Enter description  . . ."
          value={formik.values.description}
          onChange={formik.handleChange}
          error={formik.errors.description}
        />

        <Input
          label="Quantity:"
          name="quantity"
          placeholder="Enter quantity . . ."
          value={formik.values.quantity}
          onChange={formik.handleChange}
          error={formik.errors.quantity}
        />
      </div>

      <hr className="lg:col-span-2 my-2" />

      <div className="lg:col-span-2">
        <label className="form-control-label mb-2">
          Shop Images *
        </label>
        <ComboImages
          //images={formik.values.images}
          images={listImages}
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


export function ComboAdd({ shopId,combo = { name: "" }, handleClose }) {
  const [state, requestAdd] = useAPIRequest(addCombo);

  const formik = useFormik({
    enableReinitialize: true,
    initialValues: { ...combo },
    validate: (values) => {
      let errors = {};
      if (!values.comboName || values.comboName.trim().length === 0) {
        errors.comboName = "Please enter combo name.";
      }

      if (!values.description || values.description.trim().length === 0) {
        errors.description = "Please enter description.";
      }
      if (!values.quantity || values.quantity.trim().length === 0) {
        errors.quantity = "Please enter quantity.";
      }
      return errors;
    },
    validateOnBlur: false,
    validateOnChange: false,
    onSubmit: (values) => {
      requestAdd({
        shopId:shopId,
        combo:values
      });
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
          label="Combo Name:"
          name="comboName"
          placeholder="Enter name . . ."
          value={formik.values.comboName}
          onChange={formik.handleChange}
          error={formik.errors.comboName}
        />

        <Input
          label="Description:"
          name="description"
          placeholder="Enter description  . . ."
          value={formik.values.description}
          onChange={formik.handleChange}
          error={formik.errors.description}
        />

        <Input
          label="Quantity:"
          name="quantity"
          placeholder="Enter quantity . . ."
          value={formik.values.quantity}
          onChange={(e) => {
            if (!isNaN(e.target.value)) {
              formik.handleChange(e);
            }
          }}
        />

      </div>

      <hr className="lg:col-span-2 my-2" />

      <div className="lg:col-span-2">
        <label className="form-control-label mb-2">
          Combo Images *
        </label>
        <ComboImages
          images={formik.values.images}
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

export function ComboPrice({combo,handleClose}){
  const [showEdit, setShowEdit] = useState(false);
  const [showAdd, setShowAdd] = useState(false);
  const [showConfirm, setShowConfirm] = useState(false);

  const loadingContext = useContext(LoadingContext);

  const [list, setList] = useState([]);
  const [deleteId, setDeleteId] = useState();
  const [price,setPrice]=useState();
  const [listState, requestPrices] = useAPIRequest(getPrices);
  const [delState, requestDelete] = useAPIRequest(deletePrice);

  useEffect(() => {
    requestPrices(combo.id);

    return () => {
      loadingContext.setLoading(false);
    };
  }, []);

  useEffect(() => {
    loadingContext.setLoading(listState.status === Actions.loading);
    if (listState.status === Actions.success) {
      setList(listState.payload ?? []);
    }
  }, [listState]);

//#endregion

//#region Delete Price

useEffect(() => {
    loadingContext.setLoading(delState.status === Actions.loading);
    if (delState.status === Actions.success) {
      toast.success("Product deleted successfully.");
      requestPrices(combo.id);
    }
    if (delState.status === Actions.failure) {
      toast.error(parseError(delState.error));
    }
  }, [delState]);

//#endregion

  function getActionButtons(c) {
    return (
      <div className="flex space-x-2">
        <PrimaryButton
          onClick={() => {
            setPrice(c);
            setShowEdit(true);
          }}
        >
          <PencilAltIcon className="w-4 h-4" />
        </PrimaryButton>
        <DangerButton
          onClick={() => {
            setDeleteId(c.id);
            setShowConfirm(true);
          }}
        >
          <TrashIcon className="w-4 h-4" />
        </DangerButton>
      </div>
    );
  }

  return (
    <div className="flex flex-col space-y-4">

      <Modal title="Add *" isOpen={showAdd}>
        <PriceAdd
          comboId={combo.id}
          handleClose={(result) => {
            setShowAdd(false);
            if (result === true) {
              toast.success("Category save successfully.");
              requestPrices(combo.id);
            }
          }}
        />
      </Modal>

      <Modal title="Edit *" isOpen={showEdit}>
        <PriceEdit
          price={price}
          handleClose={(result) => {
            setShowEdit(false);
            setPrice(undefined);
            if (result === true) {
              toast.success("Category save successfully.");
              requestPrices(combo.id);
            }
          }}
        />
      </Modal>

      <ConfirmModal
        message="Are you sure to delete?"
        isOpen={showConfirm}
        handleClose={(result) => {
          setShowConfirm(false);
          if (result) {
            requestDelete(deleteId);
            requestPrices(combo.id);
          }
          setDeleteId(undefined);
        }}
      />

      {listState.status === Actions.failure && (
        <Alert alertClass="alert-error mb-4" closeable>
          {parseError(listState.error)}
        </Alert>
      )}

      <Card>
        <Card.Header>
          <div className="flex items-center">
            <h3 className="text-gray-600">Sub-Product</h3>
            {
              Role==='Owner'? <PrimaryButton
              className="ml-auto"
              onClick={() =>
                 setShowAdd(true)
                }
            >
              <PlusIcon className="w-5 h-5 mr-2" />
              Add New
            </PrimaryButton>
            :null
            }
            
          </div>
        </Card.Header>
        <Card.Body>
          <div className="overflow-x-auto">
            <Table>
              <Table.THead>
                <tr>
                  <Table.TH className="w-40">Duration</Table.TH>
                  <Table.TH className="w-40">Deposit</Table.TH>
                  <Table.TH className="w-40">Rental Price</Table.TH>
                  {
                    Role==='Owner'?<Table.TH className="w-44"></Table.TH>:null
                  }
                  
                </tr>
              </Table.THead>

              <Table.TBody>
                {list.map((p) => {
                  return (
                    <tr key={p.id}>
                      <Table.TD>{p.duration} days</Table.TD>
                      <Table.TD>{p.deposit}</Table.TD>
                      <Table.TD>{p.rentalPrice}</Table.TD>
                      {
                        Role==='Owner'?<Table.TD>{getActionButtons(p)}</Table.TD>:null
                      }                 
                    </tr>
                  );
                })}
              </Table.TBody>
            </Table>
          </div>
        </Card.Body>
      </Card>
          <div className="flex flex-row-reverse space-x-reverse space-x-2">
            <DefaultButton onClick={handleClose}>
                Cancel
            </DefaultButton>
          </div> 
    </div>
  );
}