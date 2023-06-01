using System.Collections;
using UnityEngine;
using DG.Tweening;
public class GateScale : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            StartCoroutine(ScalePunch());
        }
    }
    IEnumerator ScalePunch()
    {
        yield return new WaitForSeconds(.35f);
        if (transform.localScale.x<2f && transform.localScale.y<2.2f && transform.localScale.z<.2f)
        {
            transform.DOPunchScale(new Vector3(.1f, .1f, .1f), .2f, 0, .5f);
        }
    }
}