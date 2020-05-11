﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * @Author: l hy 
 * @Date: 2019-12-16 23:05:55 
 * @Description: 工具类
 * @Last Modified by: l hy
 * @Last Modified time: 2020-05-11 16:53:22
 */

public class Util {

    private static float getAspect () {
        float aspect = (float) Screen.width / Screen.height;
        return aspect;
    }

    /// <summary>
    /// 是否横屏
    /// </summary>
    /// <returns></returns>
    public static bool isLandscape () {
        float aspect = getAspect ();
        if (aspect > 1) {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 是否是ipad分辨率
    /// </summary>
    /// <returns></returns>
    public static bool isPadResoluation () {
        return targetValue (4 / 3.0f);
    }

    /// <summary>
    /// 是否是iphone分辨率
    /// </summary>
    /// <returns></returns>
    public static bool isPhone () {
        return targetValue (16 / 9.0f);
    }

    private static bool targetValue (float value) {
        float offset = 0.05f;
        float aspect = getAspect ();
        if (aspect > value - offset && aspect < value + offset) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 归并排序
    /// </summary>
    /// <param name="targetArray">目标数组</param>
    public static void mergeSort (List<double> targetArray) {
        mSort (targetArray, 0, targetArray.Count - 1);
    }

    private static void mSort (List<double> array, int l, int r) {
        if (l >= r) {
            return;
        }

        // right move calculate
        int mid = (l + r) >> 1;
        mSort (array, l, mid);
        mSort (array, mid + 1, r);
        merge (array, l, mid, r);
    }

    private static void merge (List<double> array, int l, int mid, int r) {
        // List<T> 避免ArrayList 频繁的装箱拆箱操作
        // List 需要有元素时才能使用下标进行访问
        // List<double> temp = new List<double> (r - l + 1);
        double[] temp = new double[r - l + 1];
        int leftPointer = l;
        int rightPointer = mid + 1;
        int index = 0;
        while (leftPointer <= mid && rightPointer <= r) {
            temp[index++] = array[leftPointer] <= array[rightPointer] ? array[leftPointer++] : array[rightPointer++];
        }

        while (leftPointer <= mid) {
            temp[index++] = array[leftPointer++];
        }

        while (rightPointer <= r) {
            temp[index++] = array[rightPointer++];
        }

        for (int i = 0; i < temp.Length; i++) {
            array[l + i] = temp[i];
        }
    }

    private static void qSort (List<double> array, int low, int high) {
        if (low >= high) {
            return;
        }

        int i = low, j = high;
        double key = array[i];

        while (i < j) {
            while (i < j && array[j] >= key) j--;
            array[i] = array[j];
            while (i < j && array[i] <= key) i++;
            array[j] = array[i];
        }
        array[i] = key;
        // 需要注意的是：一趟快排之后左侧基准值已经达到正确位置，以一趟快排后的i为界，二分排序
        qSort (array, low, i - 1);
        qSort (array, i + 1, high);
    }

    /// <summary>
    /// 快速排序
    /// </summary>
    /// <param name="array">目标数组</param>
    public static void qucikSort (List<double> array) {
        qSort (array, 0, array.Count - 1);
    }

    /// <summary>
    /// 简单2d射线检测（只返回是否检测到目标）      
    /// </summary>
    /// <param name="startPos">开始位置</param>
    /// <param name="direcition">方向</param>
    /// <param name="distance">距离</param>
    /// <param name="layerMask">检测层</param>
    /// <returns></returns>
    public static bool ray2DCheck (Vector2 startPos, Vector2 direcition, float distance, int layerMask) {
        RaycastHit2D raycastInfo = Physics2D.Raycast (startPos, direcition, distance, layerMask);
        if (raycastInfo) {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 绘制线段
    /// </summary>
    /// <param name="startPos">开始位置</param>
    /// <param name="direcition">方向</param>
    /// <param name="distance">距离</param>
    /// <param name="color">颜色</param>
    public static void drawLine (Vector3 startPos, Vector3 direcition, float distance, Color color) {
        Vector3 endPos = startPos + direcition * distance;
        Debug.DrawLine (startPos, endPos, color);
    }
}