using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostumeWearPanel : MonoBehaviour
{
    [Header("Datas")]
    public OwnedCostumesData ownedCostumesData;
    public PlayerProgressData playerProgressData;

    [Header("Cell")]
    public GameObject cellPrefab;

    [Header("Options View")]
    public Transform materialContentTransform;
    public Transform spearContentTransform;

    [Header("Options Material Slot")]
    public Image material1SlotImage;
    public Image material2SlotImage;
    public Image material1SlotBackgroudImage;
    public Image material2SlotBackgroudImage;
    public Button material1SlotButton;
    public Button material2SlotButton;
    private int selelectSlotIndex = 0;
    private OwnedMaterial selectedOwnedMaterial1Slot;
    private OwnedMaterial selectedOwnedMaterial2Slot;
    private Material selectedMaterial1Slot;
    private Material selectedMaterial2Slot;
    private OwnedSpear selectedOwnedSpear;

    [Header("Spear")]
    public SpearDressing spearDressing;
    public GameObject gameObjectSpear;


    private SpearDress newSpearDress;

    private List<CostumeWearCell> ownedSpears = new List<CostumeWearCell>();

    private List<CostumeWearCell> slot1Cells = new List<CostumeWearCell>();
    private List<CostumeWearCell> slot2Cells = new List<CostumeWearCell>();

    private float rotationSpeed = 50f; // Rotation speed factor
    private float smoothTime = 0.5f; // Smooth time factor

    private float currentRotationSpeed;
    private float rotationVelocity;
    private PlayerControls inputActions;

    private bool IsCostumePanelActive = false;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void SpearRotateXYZ()
    {
        float targetRotationSpeedX = 0f;
        float targetRotationSpeedY = 0f;
        float targetRotationSpeedZ = 0f;

        // Check for mouse input
        Vector2 lookInput = inputActions.Player.Look.ReadValue<Vector2>();
        if (lookInput != Vector2.zero)
        {
            targetRotationSpeedX = -lookInput.y * rotationSpeed;
            targetRotationSpeedY = -lookInput.x * rotationSpeed;
        }

        // Check for touch input
        Vector2 touchInput = inputActions.Player.Touch.ReadValue<Vector2>();
        if (touchInput != Vector2.zero)
        {
            targetRotationSpeedX = -touchInput.y * rotationSpeed;
            targetRotationSpeedY = -touchInput.x * rotationSpeed;
        }

        // Smoothly interpolate the rotation speed
        currentRotationSpeed = Mathf.SmoothDamp(currentRotationSpeed, targetRotationSpeedY, ref rotationVelocity, smoothTime);

        // Apply the rotation
        gameObjectSpear.transform.Rotate(Vector3.right, targetRotationSpeedX * Time.deltaTime);
        gameObjectSpear.transform.Rotate(Vector3.up, currentRotationSpeed * Time.deltaTime);
        gameObjectSpear.transform.Rotate(Vector3.forward, targetRotationSpeedZ * Time.deltaTime);
    }

    void Start()
    {
        CreateOptionsCell();
        IsCostumePanelActive = true;

        SpearDress defaultSpearDress = playerProgressData.spearDress;
        spearDressing.StartSpearDressing(defaultSpearDress.mesh, defaultSpearDress.materials);
        selectedMaterial1Slot = defaultSpearDress.materials[0];
        selectedMaterial2Slot = defaultSpearDress.materials[1];
        material1SlotImage.material = selectedMaterial1Slot;
        material2SlotImage.material = selectedMaterial2Slot;
        SetMaterialSlot(0);

        newSpearDress = defaultSpearDress;

        material1SlotButton.onClick.AddListener(() => SetMaterialSlot(0));
        material2SlotButton.onClick.AddListener(() => SetMaterialSlot(1));
    }

    void Update()
    {
        //if (IsCostumePanelActive) SpearRotateXYZ();
    }

    void CreateOptionsCell()
    {
        // Clear the content transforms
        foreach (Transform child in materialContentTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in spearContentTransform)
        {
            Destroy(child.gameObject);
        }

        // Clear the lists
        slot1Cells.Clear();
        slot2Cells.Clear();
        ownedSpears.Clear();

        // Create material cells
        foreach (OwnedMaterial ownedMaterial in ownedCostumesData.ownedMaterials)
        {
            CostumeWearCell costumeWearCell = Instantiate(cellPrefab, materialContentTransform).GetComponent<CostumeWearCell>();
            costumeWearCell.SetCostumeWearCell(ownedMaterial, this, ProductType.Material);
            slot1Cells.Add(costumeWearCell);
        }

        foreach (OwnedMaterial ownedMaterial in ownedCostumesData.ownedMaterials)
        {
            CostumeWearCell costumeWearCell = Instantiate(cellPrefab, materialContentTransform).GetComponent<CostumeWearCell>();
            costumeWearCell.SetCostumeWearCell(ownedMaterial, this, ProductType.Material);
            slot2Cells.Add(costumeWearCell);
        }

        // Create spear cells
        foreach (OwnedSpear ownedSpear in ownedCostumesData.ownedSpears)
        {
            CostumeWearCell costumeWearCell = Instantiate(cellPrefab, spearContentTransform).GetComponent<CostumeWearCell>();
            costumeWearCell.SetCostumeWearCell(ownedSpear, this, ProductType.Object);
            ownedSpears.Add(costumeWearCell);
        }
    }

    void SetMaterialSlot(int slotIndex)
    {
        selelectSlotIndex = slotIndex;

        if (slotIndex == 0)
        {
            material1SlotBackgroudImage.color = Color.green;
            material2SlotBackgroudImage.color = Color.white;
            material1SlotButton.interactable = false;
            material2SlotButton.interactable = true;

            for (int i = 0; i < slot1Cells.Count; i++)
            {
                slot1Cells[i].gameObject.SetActive(true);
                slot2Cells[i].gameObject.SetActive(false);
            }
        }
        else
        {
            material1SlotBackgroudImage.color = Color.white;
            material2SlotBackgroudImage.color = Color.green;
            material1SlotButton.interactable = true;
            material2SlotButton.interactable = false;

            for (int i = 0; i < slot1Cells.Count; i++)
            {
                slot1Cells[i].gameObject.SetActive(false);
                slot2Cells[i].gameObject.SetActive(true);
            }
        }
    }

    public void SelectOwnedSpear(CostumeWearCell costumeWearCell, OwnedSpear ownedSpear)
    {
        selectedOwnedSpear = ownedSpear;
        newSpearDress.mesh = selectedOwnedSpear.spearObject;

        foreach (CostumeWearCell item in ownedSpears)
        {
            if (item == costumeWearCell) continue;
            item.ResetChoose();
        }

        Dressing();
    }

    public void SelectOwnedMaterial(CostumeWearCell costumeWearCell, OwnedMaterial ownedMaterial)
    {
        if (selelectSlotIndex == 0)
        {
            selectedOwnedMaterial1Slot = ownedMaterial;
            material1SlotImage.material = selectedOwnedMaterial1Slot.materialObject;

            foreach (CostumeWearCell item in slot1Cells)
            {
                if (item == costumeWearCell) continue;
                item.ResetChoose();
            }
        }
        else
        {
            selectedOwnedMaterial2Slot = ownedMaterial;
            material2SlotImage.material = selectedOwnedMaterial2Slot.materialObject;

            foreach (CostumeWearCell item in slot2Cells)
            {
                if (item == costumeWearCell) continue;
                item.ResetChoose();
            }
        }

        Dressing();
    }

    private void Dressing()
    {
        if (selectedOwnedMaterial1Slot == null || selectedOwnedMaterial2Slot == null) return;

        Material[] materials = { selectedOwnedMaterial1Slot.materialObject, selectedOwnedMaterial2Slot.materialObject };
        spearDressing.StartSpearDressing(newSpearDress.mesh, materials.ToList());
    }
}