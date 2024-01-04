using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : MonoBehaviour
{
    public static CinemachineManager instance;
    
    private CinemachineVirtualCamera myCamera;

    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;

    private float tiempoMovimiento;
    private float tiempoMovimientoTotal;
    private float intensidadInicial;

    private void Awake()
    {
        instance = this;
        
        myCamera = GetComponent<CinemachineVirtualCamera>();

        _cinemachineBasicMultiChannelPerlin = myCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }

    public void MoverCamara(float intensidad, float frecuencia, float tiempo)
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensidad;
        _cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frecuencia;
        intensidadInicial = intensidad;
        tiempoMovimientoTotal = tiempo;
        tiempoMovimiento = tiempo;
    }

    void Update()
    {
        if(tiempoMovimiento > 0)
        {
            tiempoMovimiento -= Time.deltaTime;
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(intensidadInicial, 0, 1 - (tiempoMovimiento / tiempoMovimientoTotal));
        }
    }
}
