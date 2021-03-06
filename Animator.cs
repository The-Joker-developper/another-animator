using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{
    [SerializeField] private Animation[] animationArray;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string startAnimation;
    public int counter; 
    public Dictionary<string, Animation> animations = new Dictionary<string,Animation>();
    public IEnumerator animate;
    public string current;
    

    private void Awake()
    {
        foreach (Animation a in animationArray )
        {
            animations.Add(a.name, a);
        }
     
    }

    private void Start()
    {
        if (startAnimation != "none")
        {
            SetAnimation(startAnimation);
        }
    }

    public IEnumerator Animate(string name)
    {
        animations.TryGetValue(name, out Animation a);
        spriteRenderer.sprite = a.sprites[counter];
        yield return new WaitForSeconds(a.interval);

        counter++;

        if (counter>=a.sprites.Length)
            counter = 0;
        

        StartCoroutine(Animate(name));
    }
    public void SetOneSpriteAnimation (string name)
    {
        animations.TryGetValue(name, out Animation a);
        if (animate != null)
        {
            StopAnimation();
        }
        spriteRenderer.sprite = a.sprites[0];
        current = a.name;
      
    }
    public void StopAnimation ()
    {
        StopAllCoroutines();
        animate = null;
    }

    public void SetAnimation (string name)
    {
        if (animate!=null)
        {
            StopAnimation();
        }
        counter = 0;
        current = name;
        animate = Animate(current);
        StartCoroutine(animate);
    }

   
}
