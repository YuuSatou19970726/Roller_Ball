using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampRectTransform : MonoBehaviour
{
    [SerializeField]
    private float padding = 5.0f;
    [SerializeField]
    private float elementSize = 128.0f;
    [SerializeField]
    private float viewSize = 235.0f;
    [SerializeField]
    private float leftPadding = 15.0f;

    private RectTransform rectTransform;
    private int amountElements;
    private float contentSize;

    // Start is called before the first frame update
    void Start()
    {
        this.rectTransform = GetComponent<RectTransform>();
        this.rectTransform.localPosition = new Vector3(100, this.rectTransform.localPosition.y, this.rectTransform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        this.amountElements = this.rectTransform.childCount;
        this.contentSize =
        ((this.amountElements * (this.elementSize + this.padding)) - this.viewSize) * this.rectTransform.localScale.x;

        if (this.rectTransform.localPosition.x > this.padding + this.leftPadding)
            this.rectTransform.localPosition = new Vector3(this.padding + this.leftPadding, this.rectTransform.localPosition.y, this.rectTransform.localPosition.z);
        else if (this.rectTransform.localPosition.x < -this.contentSize)
            this.rectTransform.localPosition = new Vector3(-this.contentSize, this.rectTransform.localPosition.y, this.rectTransform.localPosition.z);
    }
}
